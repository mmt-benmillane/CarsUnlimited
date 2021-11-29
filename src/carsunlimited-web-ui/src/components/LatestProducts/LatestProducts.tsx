import { Grid, Typography } from "@mui/material";
import React, { useEffect, useState } from "react";
//import ProductCard from "../ProductCard/ProductCard";
//import InventoryItem from "../../models/InventoryItem";
//import styles from "./LatestProducts.module.css";

type LatestProductsProps = {
  name: string;
};

function LatestProducts({ name }: LatestProductsProps) {
  const [error, setError] = useState(null);
  const [isLoaded, setIsLoaded] = useState(false);
  const [items, setItems] = useState([]);

  useEffect(() => {
    fetch("{process.env.REACT_APP_API_URL}/Inventory/{name}/latest")
      .then((res) => res.json())
      .then(
        (result) => {
          setIsLoaded(true);
          setItems(result);
        },
        (error) => {
          setIsLoaded(true);
          setError(error);
        }
      );
  }, []);

  if (error) {
    return <div>Error: {error}</div>;
  } else if (!isLoaded) {
    return <div>Loading...</div>;
  } else {
    return (
      <Grid container spacing={3}>
        <Grid item xs={12}>
          <Typography
            variant="h6"
            color="inherit"
            noWrap
            sx={{ borderBottom: 1, borderColor: "divider" }}
            style={{ textTransform: "uppercase" }}
          >
            <strong>LATEST {name}</strong>
          </Typography>
        </Grid>
        {items.map((item) => (     
          console.log(item)   
          // <div>
          //   <Grid item xs>
          //     <ProductCard manufacturer={item} model={item}/>
          //   </Grid>
          // </div>
        ))}
      </Grid>
    );
  }
}

export default LatestProducts;
