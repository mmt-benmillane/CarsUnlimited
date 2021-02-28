FROM golang:1.16.0-alpine AS build
WORKDIR /src
COPY . .
RUN go mod download
RUN go build -o /out/carsunlimited-purchase-api .


FROM build AS final
COPY --from=build /out/carsunlimited-purchase-api /
EXPOSE 8080
CMD ["./out/carsunlimited-purchase-api"]