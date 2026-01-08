
var location string = resourceGroup().location
param environment string

resource keyVault 'Microsoft.KeyVault/vaults@2025-05-01' = {
  name: 'kv-${uniqueString(resourceGroup().id)}-${environment}'
  location: location
  properties: {
    tenantId: tenant().tenantId
    sku: {
      name: 'standard'
      family: 'A'
    }
    accessPolicies: []
  }
}

output keyVaultId string = keyVault.id
output keyVaultUri string = keyVault.properties.vaultUri
output keyVaultName string = keyVault.name
