// @ts-ignore
import Fade from "react-reveal/Fade";
// @ts-ignore
import Zoom from "react-reveal/Zoom";
import { useState, useEffect } from "react";

import "../assets/css/programs.css";
import PaymentOptions from "../components/payment-options";
import Search from "../components/search";
import CourseCardGridView from "../components/course-card-grid-view";
import { getMethods, getCourses } from "../helpers/api";

export interface AppProgramsPageProps {}

const AppProgramsPage: React.SFC<AppProgramsPageProps> = (props: any) => {
  const course = props?.location?.state?.data ? props.location.state.data : [];
  let searchData = props.location?.state?.searchInput;

  const [programs, setPrograms] = useState(course || []);
  const [programs_store, setPrograms_store] = useState(course || []);
  const [pageStatus, setPageStatus] = useState("loading");

  const searchCourse = (searchInput: string) => {
    if (searchInput.trim() === "") return setPrograms(programs_store);

    let courses = programs_store.filter((course: any) => {
      return course.name.toUpperCase().includes(searchInput.toUpperCase());
    });
    setPageStatus("No data");
    setPrograms(courses);
  };

  useEffect(() => {
    const fetchPrograms = async () => {
      const tokenData = await getMethods("/Edulearn/token");

      if (tokenData.status) {
        const loginData = await getCourses(tokenData.token);

        if (loginData.status) {
          setPrograms(loginData.data);
          setPrograms_store(loginData.data);
          setPageStatus("data");
        } else {
          setPageStatus("data");
        }

        if (searchData) {
          searchCourse(searchData);
        }
      } else {
        setPageStatus("data");
      }
    };

    fetchPrograms();
  }, []);

  return (
    <main id="content" role="main">
      <div>
        <div className="hero-page-about">
          <h1 className="d-none d-sm-block animated slideInDown">
            Featured Courses
          </h1>

          <p className="d-none d-sm-block animated slideInUp">
            With a wide range of courses to choose from, you are guaranteed to
            be learning from the <br /> best, regardless of your chosen field.
          </p>

          <br />

          <h4 className="d-block d-sm-none animated slideInDown">
            Featured Courses
          </h4>

          <p
            style={{ fontSize: ".9em" }}
            className="d-block d-sm-none animated slideInUp"
          >
            With a wide range of courses to choose from, you are guaranteed to
            be learning from the best, regardless of your chosen field.
          </p>

          <div className="row animated slideInUp" style={{ width: "100%" }}>
            <div className="col-md-6 offset-md-3 ">
              <Search
                search={"What do you want to learn"}
                button_text={"Search"}
                onSearchSubmit={searchCourse}
                searchData={searchData}
              ></Search>
            </div>
          </div>
        </div>
        <br />
        <Zoom>
          {programs?.length > 0 ? (
            <>
              <div className="session-four container space-2 space-top-xl-2 space-bottom-lg-2">
                <section>
                  <div className="row mx-n2 mx-lg-n3">
                    <CourseCardGridView
                      grid={3}
                      programs={programs}
                    ></CourseCardGridView>
                  </div>
                </section>
              </div>
            </>
          ) : (
            <div
              className="session-four container space-2 space-top-xl-2 space-bottom-lg-2"
              style={{
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                flexDirection: "column",
              }}
            >
              <div className="fa-3x">
                {pageStatus == "loading" ? (
                  <>
                    <i
                      style={{ fontSize: "150px" }}
                      className="fas fa-spinner fa-spin "
                    ></i>
                  </>
                ) : (
                  <>
                    <i
                      style={{ fontSize: "150px" }}
                      className="fas fa-sad-cry"
                    ></i>
                  </>
                )}
              </div>
              <br />
              <br />
              {pageStatus == "loading" ? (
                <h1>Loading ...</h1>
              ) : (
                <h1>No Course Match the search</h1>
              )}
            </div>
          )}
        </Zoom>
      </div>
      <Fade left>
        <PaymentOptions></PaymentOptions>
      </Fade>
    </main>
  );
};

export default AppProgramsPage;
