package main

import (
	"carsunlimited-purchase-api/controllers"
	"github.com/gin-gonic/gin"
	"net/http"
)

func main() {

	r := gin.Default()

	r.GET("/", func(c *gin.Context) {
		c.JSON(http.StatusOK, gin.H{"data": "hello world"})
	})

	r.POST("/purchase/:id", controllers.CompletePurchase)

	r.Run()
}