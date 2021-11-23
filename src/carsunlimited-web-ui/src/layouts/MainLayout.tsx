import { Grid } from "@mui/material";
import React from "react";
import Header from "../components/Header/Header";
import Navigation from "../components/Navigation/Navigation";

import styles from "./MainLayout.module.css";

type Props = {
  children: React.ReactNode;
};

const Layout = ({ children }: Props) => {
  return (
    <div className={styles.MainLayout}>
      <Header />
      <Grid container spacing={0}>
        <Grid item xs={12}>
          <Navigation />
        </Grid>
      </Grid>
      {children}
    </div>
  );
};

export default Layout;
