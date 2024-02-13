import { useState } from 'react'
import Grid from '@mui/material/Grid';
import './App.css'

function App() {
    const [city, setCity] = useState("")
    const [weather, setWeather] = useState()

    const handleChange = (event) => {
        setCity(event.target.value);
    };

    const handleClick = async(event) => {
        // Load
        //var json = await fetch(`http://localhost:5069/WeatherForecast/London/abc`, {
        //    headers: {
        //        'Content-Type': 'application/json',
        //        'Accept': 'application/json'
        //    }
        //});
        //console.log(json);
        setWeather("{'temperature': 7.8, 'humidity': 89, 'windSpeed': 4.63, 'icon': 'https://openweathermap.org/img/wn/10d@2x.png'}");
    }

  return (
      <>
      <h1>Enter city name</h1>
      <div className="card">
          <div>
                  <input type="text" value={city.Value} onChange={handleChange} />
              </div>
              <div>
                  <button onClick={handleClick}>Search</button>
              </div>
              <Grid container>
                  <Grid item xs={4}>Temperature</Grid>
                  <Grid item xs={4}></Grid>
              </Grid>
              <Grid container>
                  <Grid item xs={4}>Humidity</Grid>
                  <Grid item xs={4}></Grid>
              </Grid>
              <Grid container>
                  <Grid item xs={4}>WindSpeed</Grid>
                  <Grid item xs={4}></Grid>
              </Grid>
              <Grid container>
                  <Grid item xs={4}>Icon</Grid>
                  <Grid item xs={4}></Grid>
              </Grid>
    </div>
    </>
  )
}

export default App
