import React from 'react'
import './assets/css/vendor.min.css'
import './assets/css/theme.min.css'
import './App.css';
import HomePage from './pages/homepage'


export interface AppProps {

}

const App: React.SFC<AppProps> = () => {
  return (
    <div className="App">

      <HomePage></HomePage>

    </div >
  );
}

export default App;
