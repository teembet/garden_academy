import React from 'react'
import { BrowserRouter as Router, Route } from 'react-router-dom'
import './assets/css/vendor.min.css'
import './assets/css/theme.min.css'
import AppHomePage from './pages/home-page'
import AppFooter from './components/footer'
import AppHeader from './components/header'
import AppAboutUsPage from './pages/about-us-page'
import AppContactUsPage from './pages/contact-us-page'
import AppProgramsPage from './pages/programs-page'
import AppCourseDetails from './pages/course-details-page'


export interface AppProps {

}

const App: React.SFC<AppProps> = () => {
  return (
    <Router>
      <div className="App">
        <AppHeader></AppHeader>

        <Route path="/" exact component={AppHomePage}></Route>
        <Route path="/about" component={AppAboutUsPage}></Route>
        <Route path="/contact" component={AppContactUsPage}></Route>
        <Route path="/programs" component={AppProgramsPage}></Route>
        <Route path="/details" component={AppCourseDetails}></Route>
        <AppFooter></AppFooter>
      </div >
    </Router>

  );
}

export default App;
