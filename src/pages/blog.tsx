import { Link } from "react-router-dom";

import blog1 from "../assets/img/blog1.svg";
import blog2 from "../assets/img/blog2.svg";
import blog3 from "../assets/img/blog3.svg";
import blog4 from "../assets/img/blog4.svg";
import person from "../assets/img/person.svg";
import { Pagination } from "react-bootstrap";
export interface AppBlogProps {}

const AppBlog: React.SFC<AppBlogProps> = () => {
  let active = 2;
  let items = [];
  for (let number = 1; number <= 5; number++) {
    items.push(
      <Pagination.Item key={number} active={number === active}>
        {number}
      </Pagination.Item>
    );
  }

  const paginationBasic = (
    <div>
      <Pagination size="sm">{items}</Pagination>
    </div>
  );

  return (
    <>
      <main>
        <div className="hero-page-about">
          <h1 className="d-none d-sm-block">Garden Academy Blog</h1>

          <p className="d-none d-sm-block">
            Check out our Blog to stay up to date with great contents
          </p>

          <h4 className="d-block d-sm-none">Garden Academy Blog</h4>

          <p className="d-block d-sm-none" style={{ fontSize: ".9em" }}>
            Check out our Blog to stay up to date with great contents
          </p>
        </div>

        <div style={{ backgroundColor: "black" }}>
          <div className="container space-bottom-2">
            <div className="row space-top-3 space-bottom-2">
              <div className=" col-lg-12">
                <div className="mb-4">
                  <h1 className="white-text">What's New</h1>
                </div>
              </div>

              <div className="row col-lg-12 mx-n2 mx-lg-n3">
                <div
                  className="col-lg-8"
                  style={{ padding: "5% 0", position: "relative" }}
                >
                  <Link
                    to={{
                      pathname: "/blog-details",
                      state: {
                        identity: "ui",
                      },
                    }}
                  >
                    <img
                      src={blog1}
                      alt=""
                      className="img-fluid img-data d-none d-lg-block"
                    />
                    <img
                      src={blog2}
                      alt=""
                      className="img-fluid img-data d-lg-none"
                    />
                  </Link>
                </div>

                <div
                  className="col-lg-4"
                  style={{ padding: "5%", position: "relative" }}
                >
                  <div className="row mx-n2 mx-lg-n3">
                    <p className="white-text">UI/UX • January 11, 2021</p>
                    <Link
                      to={{
                        pathname: "/blog-details",
                        state: {
                          identity: "ui",
                        },
                      }}
                    >
                      <h3 style={{ color: "#677788" }}>
                        User Experience Is the Most Important Metric You Aren't
                        Measuring
                      </h3>
                    </Link>

                    <p className="white-text">
                      User experience is often overlooked in website and app
                      design and, indeed, the design of many things. How many
                      times have you felt compelled to push a door only to find
                      ...
                    </p>

                    <div className="row " style={{ width: "100%" }}>
                      <div className="col-4">
                        <img
                          className="avatar img-fluid"
                          src={person}
                          alt="avatar"
                        />
                      </div>
                      <div className="col-8">
                        <p style={{ color: "white" }}>Michael Georgiou</p>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div
                className="row col-lg-12 mx-n2 mx-lg-n3"
                style={{ justifyContent: "space-between" }}
              >
                <div className="row col-lg-6 mx-n2 mx-lg-n3">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <Link
                      to={{
                        pathname: "/blog-details",
                        state: {
                          identity: "sell",
                        },
                      }}
                    >
                      <img
                        src={blog3}
                        alt=""
                        className="img-fluid img-data d-none d-lg-block"
                      />
                      <img
                        src={blog2}
                        alt=""
                        className="img-fluid img-data d-lg-none"
                      />
                    </Link>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        SELLING MORE PRODUCT • January 11, 2021
                      </p>
                      <Link
                        to={{
                          pathname: "/blog-details",
                          state: {
                            identity: "sell",
                          },
                        }}
                      >
                        <h3 style={{ color: "#677788" }}>
                          Sell More Products by Letting Your Customers Design
                          Them for You
                        </h3>
                      </Link>

                      <p className="white-text">
                        When the founder of the beauty website Into the Gloss
                        decided to create a new cosmetics line, she didn't
                        contact vendors or post ads to entice new buyers.
                        Instead she created an Instagram account -- @glossier --
                        and waited for suggestions to flood in. She mined the
                        posts her followers submitted as she developed the line.
                        Thousands of cosmetics aficionados helped her to build
                        the new company.
                      </p>

                      <div className="row" style={{ width: "100%" }}>
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ color: "white" }}>Michael R. Solomon</p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>

                <div className="row col-lg-6 mx-n2 mx-lg-n3">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <Link
                      to={{
                        pathname: "/blog-details",
                        state: {
                          identity: "best",
                        },
                      }}
                    >
                      <img
                        src={blog4}
                        alt=""
                        className="img-fluid img-data d-none d-lg-block"
                      />
                      <img
                        src={blog2}
                        alt=""
                        className="img-fluid img-data d-lg-none"
                      />
                    </Link>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        PROJECT MANAGEMENT • January 11, 2021
                      </p>
                      <Link
                        to={{
                          pathname: "/blog-details",
                          state: {
                            identity: "best",
                          },
                        }}
                      >
                        <h3 style={{ color: "#677788" }}>
                          Best Project Management Software & Tools in 2021
                        </h3>
                      </Link>

                      <p className="white-text">
                        Nowadays, project management tools are expanding their
                        functions and crossing boundaries with their combination
                        of features, further complicating the user’s selection
                        process. We built a list of the best project management
                        software applicable for different types of industry and
                        business needs to assist in this crucial selection
                        process.
                      </p>

                      <div className="row " style={{ width: "100%" }}>
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ color: "white" }}>
                            Jose Maria Delos Santos
                          </p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div
                className="row col-lg-12 mx-n2 mx-lg-n3"
                style={{ justifyContent: "space-between" }}
              >
                <div className="row col-lg-3 mx-n2 mx-lg-n3">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <Link
                      to={{
                        pathname: "/blog-details",
                        state: {
                          identity: "pmp",
                        },
                      }}
                    >
                      <img
                        src={blog1}
                        alt=""
                        className="img-fluid img-data d-none d-lg-block"
                      />
                      <img
                        src={blog2}
                        alt=""
                        className="img-fluid img-data d-lg-none"
                      />
                    </Link>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">CULTURE • January 11, 2021</p>

                      <Link
                        to={{
                          pathname: "/blog-details",
                          state: {
                            identity: "pmp",
                          },
                        }}
                      >
                        <h3 style={{ color: "#677788" }}>
                          5 Phases of Project Management (PMP)
                        </h3>
                      </Link>

                      <p className="white-text">
                        There are different schools of thought about the number
                        of phases during a project. Some claim there are 3
                        phases; others say it’s 5. At the base of it, the PMBOK
                        points out that the number of phases is. ...
                      </p>

                      <div
                        className="row"
                        style={{ marginBottom: "0 !important", width: "100%" }}
                      >
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ color: "white" }}>ROLI PATHAK</p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>

                <div className="row col-lg-3 mx-n2 mx-lg-n3">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <Link
                      to={{
                        pathname: "/blog-details",
                        state: {
                          identity: "measure",
                        },
                      }}
                    >
                      <img
                        src={blog1}
                        alt=""
                        className="img-fluid img-data d-none d-lg-block"
                      />
                      <img
                        src={blog2}
                        alt=""
                        className="img-fluid img-data d-lg-none"
                      />
                    </Link>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        MEASURE ENGAGEMENT • January 11, 2021
                      </p>

                      <Link
                        to={{
                          pathname: "/blog-details",
                          state: {
                            identity: "measure",
                          },
                        }}
                      >
                        <h3 style={{ color: "#677788" }}>
                          Measuring Engagement is Not the Same as Listening
                        </h3>
                      </Link>

                      <p className="white-text">
                        The problem is that actual listening requires
                        organizations to ask more than a few
                        statistically-relevant questions on employee surveys.
                        But more than that, ...
                      </p>

                      <div
                        className="row"
                        style={{ marginBottom: "0 !important", width: "100%" }}
                      >
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ color: "white" }}>Sarah Johnson</p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>

                <div className="row col-lg-3 mx-n2 mx-lg-n3">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <Link
                      to={{
                        pathname: "/blog-details",
                        state: {
                          identity: "emerging",
                        },
                      }}
                    >
                      <img
                        src={blog1}
                        alt=""
                        className="img-fluid img-data d-none d-lg-block"
                      />
                      <img
                        src={blog2}
                        alt=""
                        className="img-fluid img-data d-lg-none"
                      />
                    </Link>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        EMERGING VR • January 11, 2021
                      </p>

                      <Link
                        to={{
                          pathname: "/blog-details",
                          state: {
                            identity: "emerging",
                          },
                        }}
                      >
                        <h3 style={{ color: "#677788" }}>
                          Emerging VR & AR in Recruitment - The Simulation
                          process
                        </h3>
                      </Link>

                      <p className="white-text">
                        Modern enterprises are increasingly opting for new
                        technologies for enhanced efficiency. Virtual Reality
                        (VR) and Augmented Reality (AR) have now grown popular
                        for employee training,...
                      </p>

                      <div
                        className="row"
                        style={{ marginBottom: "0 !important", width: "100%" }}
                      >
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ color: "white" }}>Paul Osborne</p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>

                <div className="row col-lg-3 mx-n2 mx-lg-n3">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <Link
                      to={{
                        pathname: "/blog-details",
                        state: {
                          identity: "fintech",
                        },
                      }}
                    >
                      <img
                        src={blog1}
                        alt=""
                        className="img-fluid img-data d-none d-lg-block"
                      />
                      <img
                        src={blog2}
                        alt=""
                        className="img-fluid img-data d-lg-none"
                      />
                    </Link>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        FINTECH TREND • January 11, 2021
                      </p>

                      <Link
                        to={{
                          pathname: "/blog-details",
                          state: {
                            identity: "fintech",
                          },
                        }}
                      >
                        <h3 style={{ color: "#677788" }}>
                          Top 5 Fintech Trends That Will Shape Financial Markets
                          in 2021
                        </h3>
                      </Link>

                      <p className="white-text">
                        The year 2020 was not the greatest for many industries
                        due to COVID-19. But interestingly, the likes of fintech
                        reported rapid growth ...
                      </p>

                      <div
                        className="row"
                        style={{ marginBottom: "0 !important", width: "100%" }}
                      >
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ color: "white" }}>James Jorner</p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>

              <div
                className="row col-lg-12 mx-n2 mx-lg-n3"
                style={{ justifyContent: "space-between" }}
              >
                <div className="row col-lg-4 mx-n2 mx-lg-n3">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <Link
                      to={{
                        pathname: "/blog-details",
                        state: {
                          identity: "payment",
                        },
                      }}
                    >
                      <img
                        src={blog1}
                        alt=""
                        className="img-fluid img-data d-none d-lg-block"
                      />
                      <img
                        src={blog2}
                        alt=""
                        className="img-fluid img-data d-lg-none"
                      />
                    </Link>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        ORCHESTRATION • January 11, 2021
                      </p>

                      <Link
                        to={{
                          pathname: "/blog-details",
                          state: {
                            identity: "payment",
                          },
                        }}
                      >
                        <h3 style={{ color: "#677788" }}>
                          Three ways payment orchestration improves financial
                          reconciliation
                        </h3>
                      </Link>

                      <p className="white-text">
                        When Luca Pacioli, the 15th century Venetian monk,
                        invented double-entry account keeping, managing
                        financial reconciliations had its own unique challenges.
                        ...
                      </p>

                      <div
                        className="row"
                        style={{ marginBottom: "0 !important", width: "100%" }}
                      >
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ color: "white" }}>Brian Coburn</p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>

                <div className="row col-lg-4 mx-n2 mx-lg-n3">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <Link
                      to={{
                        pathname: "/blog-details",
                        state: {
                          identity: "artificial",
                        },
                      }}
                    >
                      <img
                        src={blog1}
                        alt=""
                        className="img-fluid img-data d-none d-lg-block"
                      />
                      <img
                        src={blog2}
                        alt=""
                        className="img-fluid img-data d-lg-none"
                      />
                    </Link>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        ARTIFICIAL INTELLIGENCE • January 11, 2021
                      </p>

                      <Link
                        to={{
                          pathname: "/blog-details",
                          state: {
                            identity: "artificial",
                          },
                        }}
                      >
                        <h3 style={{ color: "#677788" }}>
                          Using Artificial Intelligence to Improve Law Firm
                          Performance
                        </h3>
                      </Link>

                      <p className="white-text">
                        The evolution and utilization of artificial intelligence
                        (AI) is on the rise and shows no signs of stopping in
                        the near future.In fact, Statista reports that global
                        revenues from enterprise applications making use of AI
                        ...
                      </p>

                      <div
                        className="row"
                        style={{ marginBottom: "0 !important", width: "100%" }}
                      >
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ color: "white" }}>Holly Urban</p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>

                <div className="row col-lg-4 mx-n2 mx-lg-n3">
                  <div
                    className="col-lg-12"
                    style={{ padding: "5% 0", position: "relative" }}
                  >
                    <Link
                      to={{
                        pathname: "/blog-details",
                        state: {
                          identity: "habit",
                        },
                      }}
                    >
                      <img
                        src={blog1}
                        alt=""
                        className="img-fluid img-data d-none d-lg-block"
                      />
                      <img
                        src={blog2}
                        alt=""
                        className="img-fluid img-data d-lg-none"
                      />
                    </Link>
                  </div>

                  <div
                    className="col-lg-12"
                    style={{ padding: "5%", position: "relative" }}
                  >
                    <div className="row mx-n2 mx-lg-n3">
                      <p className="white-text">
                        TRANSFORMATION • January 11, 2021
                      </p>

                      <Link
                        to={{
                          pathname: "/blog-details",
                          state: {
                            identity: "habit",
                          },
                        }}
                      >
                        <h3 style={{ color: "#677788" }}>
                          Digital transformation: 11 habits of successful teams
                        </h3>
                      </Link>

                      <p className="white-text">
                        Every technology leader knows that transformation is
                        difficult, and digital transformation is especially so.
                        While nearly all IT leaders (93 percent) recently
                        surveyed by Hanover Research said that their enterprises
                        are undergoing some kind of...
                      </p>

                      <div
                        className="row"
                        style={{ marginBottom: "0 !important", width: "100%" }}
                      >
                        <div className="col-4">
                          <img
                            className="avatar img-fluid"
                            src={person}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-8">
                          <p style={{ color: "white" }}>Stephanie Overby</p>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              <div
                className="col-lg-12"
                style={{ justifyContent: "center", display: "flex" }}
              >
                {/* {paginationBasic} */}
              </div>
            </div>
          </div>
        </div>
      </main>
    </>
  );
};
export default AppBlog;
