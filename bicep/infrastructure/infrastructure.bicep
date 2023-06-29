param location string = resourceGroup().location
param baseName string = 'aca-demo'
param environmentCode string

module containerRegistry '../modules/container-registry.bicep' = {
  name: 'container-registry'
  params: {
    location: location
    containerRegistryName: '${replace(baseName, '-', '')}cr${environmentCode}'
  }
}

module serviceBusNamespace '../modules/service-bus-namespace.bicep' = {
  name: 'service-bus-namespace'
  params: {
    location: location
    serviceBusNamespaceName: '${baseName}-sbns-${environmentCode}'
  }
}
