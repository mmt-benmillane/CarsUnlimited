FROM mongo:4.4-bionic

COPY dump /home/dump
COPY mongo.sh /home/mongo.sh
RUN chmod 777 /home/mongo.sh
RUN ls -la /home

CMD /home/mongo.sh