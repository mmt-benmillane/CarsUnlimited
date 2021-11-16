import React from 'react';

import Toolbar from '@mui/material/Toolbar';
import { Typography, Button } from '@mui/material';

import '../../helpers/FontAwesome';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';

export default function Header() {

  return (
    <React.Fragment>
      <Toolbar sx={{ borderBottom: 1, borderColor: 'divider' }}>
        <Typography variant="h6" color="inherit" noWrap sx={{ flexGrow: 1 }}>
            <img src="/images/logo.png" alt="Cars Unlimited Logo" width="250" />
        </Typography>                  
        <Button variant="outlined" startIcon={<FontAwesomeIcon icon={['fas', 'shopping-basket']} />}>
          0
        </Button>
      </Toolbar>     
    </React.Fragment>
  );
}
