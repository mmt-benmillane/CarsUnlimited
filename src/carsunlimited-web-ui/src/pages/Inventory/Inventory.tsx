import React from "react";

import Layout from "../../layouts/MainLayout";

import styles from "./Inventory.module.css";

type Props = {
  category?: string
};

const Inventory = ({ category = "Car"}: Props) => (
  <Layout>
    <div className={styles.Inventory} data-testid="Inventory">
      { category }
    </div>
  </Layout>
);

export default Inventory;
