param location string
param logWorkspaceName string
param appInsightsName string
param environmentName string
param daprComponents array = []

resource logsWorkspace 'Microsoft.OperationalInsights/workspaces@2021-06-01' = {
  name: logWorkspaceName
  location: location
  properties: any({
    retentionInDays: 30
    features: {
      searchVersion: 1
    }
    sku: {
      name: 'PerGB2018'
    }
  })
}

resource appInsights 'Microsoft.Insights/components@2020-02-02' = {
  name: appInsightsName
  location: location
  kind: 'web'
  properties: {
    Application_Type: 'web'
    WorkspaceResourceId: logsWorkspace.id
  }
}

resource env 'Microsoft.App/managedEnvironments@2022-10-01' = {
  name: environmentName
  location: location
  sku: {
    name: 'Consumption'
  }
  properties: {
    daprAIConnectionString: appInsights.properties.ConnectionString
    daprAIInstrumentationKey: appInsights.properties.InstrumentationKey
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logsWorkspace.properties.customerId
        sharedKey: logsWorkspace.listKeys().primarySharedKey
      }
    }
  }

  resource components 'daprComponents@2022-10-01' = [for component in daprComponents: {
    name: component.name
    properties: component.properties
  }]
}

output id string = env.id
output appInsightsInstrumentationKey string = appInsights.properties.InstrumentationKey
output appInsightsConnectionString string = appInsights.properties.ConnectionString
