import React from "react";
import { useParams } from "react-router-dom";
import Layout from "../../layouts/MainLayout";
import styles from "./Product.module.css";

function Product() {

  let { manufacturer, model } = useParams();

  return (
    <Layout>
      <div className={styles.Product} data-testid="Product">
        {manufacturer} {model}
      </div>
    </Layout>
  );
}
 export default Product;