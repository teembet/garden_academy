import React from "react";
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";
import AppHomePage from "./pages/home-page";
import AppFooter from "./components/footer";
import AppHeader from "./components/header";
import AppAboutUsPage from "./pages/about-us-page";
import AppContactUsPage from "./pages/contact-us-page";
import AppProgramsPage from "./pages/programs-page";
import AppCourseDetails from "./pages/course-details-page";
import NotFoundPage from "./pages/404-page";

import ScrollToTop from "./helpers/scrollToTheTop";

export interface AppProps {}

const App: React.SFC<AppProps> = () => {
  return (
    <Router>
      <ScrollToTop />

      <div className="App">
        <AppHeader></AppHeader>
        <Switch>
          <Route path="/" exact component={AppHomePage}></Route>
          <Route path="/about" component={AppAboutUsPage}></Route>
          <Route path="/contact" component={AppContactUsPage}></Route>
          <Route path="/programs" component={AppProgramsPage}></Route>
          <Route path="/details" component={AppCourseDetails}></Route>
          <Route path="" component={NotFoundPage}></Route>
        </Switch>
        <AppFooter></AppFooter>
      </div>
    </Router>
  );
};

export default App;
