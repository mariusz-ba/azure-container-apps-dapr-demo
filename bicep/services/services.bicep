param location string = resourceGroup().location
param baseName string = 'aca-demo'
param environmentCode string

param serviceAImage string
param serviceBImage string
param gatewayImage string

resource containerRegistry 'Microsoft.ContainerRegistry/registries@2022-12-01' existing = {
  name: '${replace(baseName, '-', '')}cr${environmentCode}'
}

resource serviceBusAuthorizationRule 'Microsoft.ServiceBus/namespaces/AuthorizationRules@2021-11-01' existing = {
  name: '${baseName}-sbns-${environmentCode}/ContainerAppsAccessKey'
}

module containerAppEnvironment '../modules/container-app-environment.bicep' = {
  name: 'environment'
  params: {
    location: location
    logWorkspaceName: '${baseName}-logs-${environmentCode}'
    appInsightsName: '${baseName}-ai-${environmentCode}'
    environmentName: '${baseName}-env-${environmentCode}'
    daprComponents: [
      {
        name: 'message-broker'
        properties: {
          componentType: 'pubsub.azure.servicebus.topics'
          version: 'v1'
          secrets: [
            {
              name: 'service-bus-connection-string'
              value: serviceBusAuthorizationRule.listKeys().primaryConnectionString
            }
          ]
          metadata: [
            {
              name: 'connectionString'
              secretRef: 'service-bus-connection-string'
            }
          ]
          scopes: [
            'aca-demo-service-a'
            'aca-demo-service-b'
          ]
        }
      }
      {
        name: 'cron-send-notification'
        properties: {
          componentType: 'bindings.cron'
          version: 'v1'
          metadata: [
            {
              name: 'route'
              value: '/dapr/cron/send-notification'
            }
            {
              name: 'schedule'
              value: '0 * * * * *'
            }
          ]
          scopes: [
            'aca-demo-service-b'
          ]
        }
      }
    ]
  }
}

var commonEnvironmentVariables = [
  {
    name: 'ASPNETCORE_ENVIRONMENT'
    value: 'Production'
  }
  {
    name: 'APPINSIGHTS_INSTRUMENTATIONKEY'
    value: containerAppEnvironment.outputs.appInsightsInstrumentationKey
  }
  {
    name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
    value: containerAppEnvironment.outputs.appInsightsConnectionString
  }
]

module serviceA '../modules/container-app.bicep' = {
  name: 'service-a'
  params: {
    location: location
    environmentId: containerAppEnvironment.outputs.id
    containerAppName: '${baseName}-service-a-${environmentCode}'
    containerRegistry: containerRegistry.properties.loginServer
    containerRegistryUsername: containerRegistry.listCredentials().username
    containerRegistryPassword: containerRegistry.listCredentials().passwords[0].value
    containerImage: '${containerRegistry.properties.loginServer}/${serviceAImage}'
    containerPort: 80
    isExternalIngress: false
    environmentVars: commonEnvironmentVariables
    dapr: {
      enabled: true
      appId: 'aca-demo-service-a'
      appPort: 80
      appProtocol: 'http'
    }
  }
}

module serviceB '../modules/container-app.bicep' = {
  name: 'service-b'
  params: {
    location: location
    environmentId: containerAppEnvironment.outputs.id
    containerAppName: '${baseName}-service-b-${environmentCode}'
    containerRegistry: containerRegistry.properties.loginServer
    containerRegistryUsername: containerRegistry.listCredentials().username
    containerRegistryPassword: containerRegistry.listCredentials().passwords[0].value
    containerImage: '${containerRegistry.properties.loginServer}/${serviceBImage}'
    containerPort: 80
    isExternalIngress: false
    environmentVars: commonEnvironmentVariables
    dapr: {
      enabled: true
      appId: 'aca-demo-service-b'
      appPort: 80
      appProtocol: 'http'
    }
  }
}

module gateway '../modules/container-app.bicep' = {
  name: 'gateway'
  params: {
    location: location
    environmentId: containerAppEnvironment.outputs.id
    containerAppName: '${baseName}-gateway-${environmentCode}'
    containerRegistry: containerRegistry.properties.loginServer
    containerRegistryUsername: containerRegistry.listCredentials().username
    containerRegistryPassword: containerRegistry.listCredentials().passwords[0].value
    containerImage: '${containerRegistry.properties.loginServer}/${gatewayImage}'
    containerPort: 80
    isExternalIngress: true
    environmentVars: commonEnvironmentVariables
    dapr: {
      enabled: true
      appId: 'aca-demo-gateway'
      appPort: 80
      appProtocol: 'http'
    }
  }
}

