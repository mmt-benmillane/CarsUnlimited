import React from "react";
import styles from "./Navigation.module.css";

import { Toolbar, Button, Input, FormControl, InputAdornment, Typography, CssBaseline } from "@mui/material";

import '../../helpers/FontAwesome';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

const Navigation = () => (
  <div className={styles.Navigation} data-testid="Navigation">
    <CssBaseline />
    <Toolbar sx={{ borderBottom: 1, borderColor: "divider"}}>
      <Typography color="inherit" noWrap sx={{ flexGrow: 1 }}>
      <Button href="/" sx={{margin: 0}}><FontAwesomeIcon icon={['fas', 'home']} /></Button>
      <Button href="/Inventory">Cars</Button>        
      <Button href="/Inventory">Parts</Button>  
      </Typography>
      <FormControl variant="standard">
        <Input
          id="input-with-icon-adornment"
          startAdornment={
            <InputAdornment position="start">
              <FontAwesomeIcon icon={['fas', 'search']} />
            </InputAdornment>
          }
          placeholder="Search"
        />
      </FormControl>      
    </Toolbar>
  </div>
);

export default Navigation;
