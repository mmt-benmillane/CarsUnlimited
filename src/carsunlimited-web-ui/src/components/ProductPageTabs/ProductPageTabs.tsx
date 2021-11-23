import { Box, Tabs, Tab, Typography } from '@mui/material';
import React from 'react';

interface TabPanelProps {
  children?: React.ReactNode;
  index: number;
  value: number;
}

function TabPanel(props: TabPanelProps) {
  const { children, value, index, ...other } = props;

  return (
    <div
      role="tabpanel"
      hidden={value !== index}
      id={`simple-tabpanel-${index}`}
      aria-labelledby={`simple-tab-${index}`}
      {...other}
    >
      {value === index && (
        <Box sx={{ p: 3 }}>
          <Typography>{children}</Typography>
        </Box>
      )}
    </div>
  );
}

function a11yProps(index: number) {
  return {
    id: `simple-tab-${index}`,
    'aria-controls': `simple-tabpanel-${index}`,
  };
}

export default function ProductPageTabs() {
  const [value, setValue] = React.useState(0);

  const handleChange = (event: React.SyntheticEvent, newValue: number) => {
    setValue(newValue);
  };

  return (
    <Box sx={{ width: '100%' }}>
    <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
      <Tabs value={value} onChange={handleChange} aria-label="basic tabs example">
        <Tab label="Description" {...a11yProps(0)} />
        <Tab label="Review (10)" {...a11yProps(1)} />
      </Tabs>
    </Box>
    <TabPanel value={value} index={0}>
      The BMW X5 offers driving pleasure on any terrain thanks to the enhanced BMW TwinPower Turbo power unit and the outstanding traction of BMW xDrive all-wheel drive. The optional xOffroad package provides finer tuning: four selectable off-road modes adjust the all-wheel drive precisely to the current ground conditions.
    </TabPanel>
    <TabPanel value={value} index={1}>
      Item Two
    </TabPanel>
  </Box>
  );
}
