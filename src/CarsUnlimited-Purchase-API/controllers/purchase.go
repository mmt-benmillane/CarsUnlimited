package controllers

import (
	"carsunlimited-purchase-api/appsettings"
	"fmt"
	"github.com/gin-gonic/gin"
	"log"
	"net/http"

	"github.com/streadway/amqp"
)

type AppSettings struct {
	ServiceBusConnectionString string `envconfig:"optional"`
}

func failOnError(err error, msg string) {
	if err != nil {
		log.Fatalf("%s: %s", msg, err)
	}
}

// POST /purchase/:id
// Complete the purchase
func CompletePurchase(c *gin.Context) {

	settings := AppSettings{}

	// filling the variable with the settings file and env vars
	if err := appsettings.ReadFromFileAndEnv(&settings); err != nil {
		panic(err)
	}

	var id = c.Param("id")

	conn, err := amqp.Dial(settings.ServiceBusConnectionString)
	failOnError(err, "Failed to connect to RabbitMQ")
	defer conn.Close()

	ch, err := conn.Channel()
	failOnError(err, "Failed to open a channel")
	defer ch.Close()

	q, err := ch.QueueDeclare(
		"hello", // name
		false,   // durable
		false,   // delete when unused
		false,   // exclusive
		false,   // no-wait
		nil,     // arguments
	)
	failOnError(err, "Failed to declare a queue")

	body := fmt.Sprintf("PURCHASE THE THINGS with %v", id)
	err = ch.Publish(
		"",     // exchange
		q.Name, // routing key
		false,  // mandatory
		false,  // immediate
		amqp.Publishing{
			ContentType: "text/plain",
			Body:        []byte(body),
		})
	failOnError(err, "Failed to publish a message")
	log.Printf(" [x] Sent %s", body)

	c.JSON(http.StatusOK, gin.H{"data": body})
}