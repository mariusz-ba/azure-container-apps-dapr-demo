param location string
param serviceBusNamespaceName string

resource serviceBusNamespace 'Microsoft.ServiceBus/namespaces@2021-11-01' = {
  name: serviceBusNamespaceName
  location: location
  sku: {
    name: 'Standard'
    tier: 'Standard'
  }

  resource containerAppsAuthorizationRule 'AuthorizationRules@2021-11-01' = {
    name: 'ContainerAppsAccessKey'
    properties: {
      rights: [
        'Manage'
        'Listen'
        'Send'
      ]
    }
  }
}
