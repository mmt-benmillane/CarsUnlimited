import React from "react";

import Layout from "../../layouts/MainLayout";

import styles from "./Inventory.module.css";

const Inventory = () => (
  <Layout>
    <div className={styles.Inventory} data-testid="Inventory">
      Inventory Component
    </div>
  </Layout>
);

export default Inventory;
