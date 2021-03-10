FROM mongo:4.4-bionic

COPY CarsUnlimited.InventoryAPI/Mongo/dump /home/dump
COPY CarsUnlimited.InventoryAPI/Mongo/mongo.sh /home/mongo.sh
RUN chmod 777 /home/mongo.sh
CMD /home/mongo.sh