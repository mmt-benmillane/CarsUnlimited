#!/bin/bash

# Initialize a mongo data folder and logfile
mkdir -p /data/db

# Wait until mongo logs that it's ready (or timeout after 60s)
COUNTER=0
grep -q 'waiting for connections on port' /var/log/mongodb.log
while [[ $? -ne 0 && $COUNTER -lt 5 ]] ; do
    sleep 2
    let COUNTER+=2
    echo "Waiting for mongo to initialize... ($COUNTER seconds so far)"
    grep -q 'waiting for connections on port' /var/log/mongodb.log
done

# Restore from dump
mongorestore --db=CarsUnlimitedDb --collection=inventory /home/dump/inventory.bson

# Keep container running
tail -f /dev/null