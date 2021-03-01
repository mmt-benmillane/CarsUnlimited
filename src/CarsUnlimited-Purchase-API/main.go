package main

import (
	"carsunlimited-purchase-api/controllers"
	"github.com/gin-gonic/gin"
	"net/http"
)

func main() {

	r := gin.Default()

	r.GET("/", func(c *gin.Context) {
		c.Status(http.StatusNoContent)
	})

	r.POST("/api/purchase/:id", controllers.CompletePurchase)

	r.Run()
}