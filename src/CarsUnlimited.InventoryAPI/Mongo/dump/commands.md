## Dump - Within the container using exec
`mongodump --db=CarsUnlimitedDb --collection=inventory .`

## Load - Within the container using exec
`mongorestore --db=CarsUnlimitedDb --collection=inventory ./CarsUnlimitedDb/inventory.bson`

## Dump - Externally with mongocli
`mongodump --host "your-host-name" --db=CarsUnlimitedDb --collection=inventory .`

## Load - Externally with mongocli
`mongorestore --host "your-host-name" --db=CarsUnlimitedDb --collection=inventory ./CarsUnlimitedDb/inventory.bson`
