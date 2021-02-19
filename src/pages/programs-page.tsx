import { useState, useEffect } from "react";

import "../assets/css/programs.css";
import PaymentOptions from "../components/payment-options";
import Search from "../components/search";
import CourseCardGridView from "../components/course-card-grid-view";

export interface AppProgramsPageProps {}

const AppProgramsPage: React.SFC<AppProgramsPageProps> = (props: any) => {
  const course = props?.location?.state?.data ? props.location.state.data : [];
  const [programs, setPrograms] = useState([]);

  const searchCourse = () => {
    console.log("wind");
  };

  useEffect(() => {
    setPrograms(course);

    const fetchPrograms = async () => {
      const response = await fetch(
        "https://demo.vigilearnlms.com/api/all/courses",
        {
          method: "POST", // *GET, POST, PUT, DELETE, etc.
          mode: "cors", // no-cors, *cors, same-origin
          cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
          credentials: "same-origin", // include, *same-origin, omit
          headers: {
            "Content-Type": "application/json",
            // 'Content-Type': 'application/x-www-form-urlencoded',
          },
          redirect: "follow", // manual, *follow, error
          referrerPolicy: "no-referrer", // no-referrer, *no-referrer-when-downgrade, origin, origin-when-cross-origin, same-origin, strict-origin, strict-origin-when-cross-origin, unsafe-url
          body: JSON.stringify({
            username: "Tech@edutechng.com",
            password: "Password_10",
          }), // body data type must match "Content-Type" header
        }
      );

      let loginData = await response.json();
      if (loginData.status) {
        console.log(loginData.data);
        setPrograms(loginData.data);
      }
    };

    fetchPrograms();
  }, []);

  return (
    <main id="content" role="main">
      <div>
        <div className="hero-page-about">
          <h1>Featured Courses</h1>

          <p>
            Choose from any of our wide range of courses tailored to suit your
            needs.
            <br /> From software engineering to product management and more, We
            have got you covered
          </p>

          <div className="row" style={{ width: "100%" }}>
            <div className="col-md-6 offset-md-3 ">
              <Search
                search={"What do you want to learn"}
                button_text={"Search"}
                onSearchSubmit={searchCourse}
              ></Search>
            </div>
          </div>
        </div>
        <br />

        {programs?.length > 0 ? (
          <div className="session-four container space-2 space-top-xl-3 space-bottom-lg-3">
            <section>
              <div className="row mx-n2 mx-lg-n3">
                <CourseCardGridView
                  grid={3}
                  programs={programs}
                ></CourseCardGridView>
              </div>
            </section>
          </div>
        ) : (
          <div
            className="session-four container space-2 space-top-xl-3 space-bottom-lg-3"
            style={{
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
            }}
          >
            <h1>Loading ...</h1>
          </div>
        )}
      </div>
      <PaymentOptions></PaymentOptions>
    </main>
  );
};

export default AppProgramsPage;
