import * as React from "react";
import { Link } from "react-router-dom";
import { Modal, Button } from "react-bootstrap";
import { useState, useEffect } from "react";

import "../assets/css/homepage.css";
import rectangle from "../assets/img/rectangle.svg";
import facilitator from "../assets/img/facilitator.svg";
import homehero from "../assets/img/home-hero.svg";
import homehero2 from "../assets/img/home-hero2.svg";
import homecard1 from "../assets/img/homecard1.svg";
import homecard2 from "../assets/img/homecard2.svg";
import homecard3 from "../assets/img/homecard3.svg";
import homecard4 from "../assets/img/homecard4.svg";
import homecard5 from "../assets/img/homecard5.svg";
import homecard6 from "../assets/img/homecard6.svg";
import womanPic from "../assets/img/woman.svg";
import person2 from "../assets/img/person2.svg";
import person1 from "../assets/img/person1.svg";
import CourseCardGridView from "../components/course-card-grid-view";
import Search from "../components/search";

export interface AppHomePageProps {
  images: any;
}

const AppHomePage: React.SFC<AppHomePageProps> = () => {
  const searchCourse = () => {
    console.log("wind");
  };

  const [firstName, setFirstName] = useState("");
  const [firstNameValid, setFirstNameValid] = useState("");
  const [lastName, setLastName] = useState("");
  const [lastNameValid, setLastNameValid] = useState("");
  const [phone, setPhone] = useState("");
  const [phoneValid, setPhoneValid] = useState("");
  const [email, setEmail] = useState("");
  const [emailValid, setEmailValid] = useState("");
  const [url, setUrl] = useState("");
  const [urlValid, setUrlValid] = useState("");

  const [showModal, setShowModal] = useState(false);
  const [programs, setPrograms] = useState([]);

  const [images, setImages] = useState([
    {
      image: person1,
      title: "Patience Toyosi",
      social: "Facebook",
      content:
        "“Completely beautiful website and amazing support! This is my second website from this author and I love both of the sites so much and she has helped me so well when I needed it!”",
    },
    {
      image: person2,
      title: "Patience Toyosi",
      social: "Facebook",
      content:
        "“Completely beautiful website and amazing support! This is my second website from this author and I love both of the sites so much and she has helped me so well when I needed it!”",
    },

    {
      image: person1,
      title: "Patience Toyosi",
      social: "google",
      content:
        "“Completely beautiful website and amazing support! This is my second website from this author and I love both of the sites so much and she has helped me so well when I needed it!”",
    },
    {
      image: person2,
      title: "Patience Toyosi",
      social: "google",
      content:
        "“Completely beautiful website and amazing support! This is my second website from this author and I love both of the sites so much and she has helped me so well when I needed it!”",
    },
  ]);

  const [index, setIndex] = useState(0);

  const slideRight = () => {
    const nextIndex = index + 1;
    if (nextIndex > images.length - 1) {
      return;
    } else {
      setIndex(nextIndex);
    }
  };

  const slideLeft = () => {
    const nextIndex = index - 1;
    if (nextIndex < 0) {
      return;
    } else {
      setIndex(nextIndex);
    }
  };

  const handleClose = () => setShowModal(false);
  const handleShow = () => setShowModal(true);
  const handleSubmit = () => {
    if (firstNameValidation(firstName) !== true) {
      return firstNameValidation(firstName);
    }

    if (lastNameValidation(lastName) !== true) {
      return lastNameValidation(lastName);
    }

    if (phoneValidation(phone) !== true) {
      return phoneValidation(phone);
    }

    if (emailValidation(email) !== true) {
      return emailValidation(email);
    }

    if (urlValidation(url) !== true) {
      return urlValidation(url);
    }

    handleClose();
    alert("Success");
  };

  useEffect(() => {
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

  const firstNameValidation = (fieldValue: string): boolean => {
    if (fieldValue.trim() === "") {
      setFirstNameValid(`First name is required`);
      return false;
    }

    if (/[^a-zA-Z -]/.test(fieldValue)) {
      setFirstNameValid("Invalid characters");
      return false;
    }

    if (fieldValue.trim().length < 3) {
      setFirstNameValid(`First name needs to be at least three characters`);
      return false;
    }
    setFirstNameValid("");
    return true;
  };

  const lastNameValidation = (fieldValue: string): boolean => {
    if (fieldValue.trim() === "") {
      setLastNameValid(`Last name is required`);
      return false;
    }

    if (/[^a-zA-Z -]/.test(fieldValue)) {
      setLastNameValid("Invalid characters");
      return false;
    }

    if (fieldValue.trim().length < 3) {
      setLastNameValid(`Last name needs to be at least three characters`);
      return false;
    }

    setLastNameValid("");
    return true;
  };

  const emailValidation = (email: string): boolean => {
    if (
      /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(
        email
      )
    ) {
      setEmailValid("");
      return true;
    }
    if (email.trim() === "") {
      setEmailValid("Email is required");
      return false;
    }
    setEmailValid("Please enter a valid email");
    return false;
  };

  const phoneValidation = (phone: string): boolean => {
    if (/^[0]\d{10}$/.test(phone)) {
      setPhoneValid("");
      return true;
    }
    if (phone.trim() === "") {
      setPhoneValid("Phone is required");
      return false;
    }
    setPhoneValid("Please enter a valid phone");
    return false;
  };

  const urlValidation = (url: string): boolean => {
    if (
      /((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+(:[0-9]+)?|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)/.test(
        url
      )
    ) {
      setUrlValid("");
      return true;
    }
    if (url.trim() === "") {
      setUrlValid("Portfolio URL is required");
      return false;
    }
    setUrlValid("Please enter a valid URL");
    return false;
  };

  return (
    <>
      <main id="content" role="main">
        <div
          className="d-lg-flex position-relative hero"
          style={{ paddingBottom: "5%" }}
        >
          <div className="container d-lg-flex  align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
            <div className="w-md-100">
              <div className="row">
                <div className="col-lg-6">
                  <div className="mb-5 mt-11">
                    <h1 className="display-4 mb-3">
                      Learn the <span className="text-primary">skills</span> you
                      need to <span className="text-primary">succeed</span>
                      <span className="text-primary text-highlight-warning">
                        <span
                          className="js-text-animation"
                          data-hs-typed-options='{
                            "strings": ["startup.", "future.", "success."],
                            "typeSpeed": 90,
                            "loop": true,
                            "backSpeed": 30,
                            "backDelay": 2500
                          }'
                        ></span>
                      </span>
                    </h1>
                    <p className="lead">
                      {" "}
                      We are committed to training the next generation of tech
                      superstars and help organisations upscale their workforce
                      with the right talent
                    </p>
                  </div>

                  <Search
                    search={"What do you want to learn"}
                    button_text={"Search"}
                    onSearchSubmit={searchCourse}
                  ></Search>
                  <br />
                  <br />
                </div>

                <div className="col-lg-6">
                  <img
                    src={homehero}
                    alt=""
                    className="img-fluid img-fluid d-none d-lg-block"
                    style={{
                      position: "absolute",
                      right: 0,
                    }}
                  />
                  <img
                    src={womanPic}
                    alt=""
                    className="img-fluid d-lg-none"
                    style={{ width: "100%" }}
                  />
                </div>
              </div>
            </div>
          </div>
        </div>

        <div className="container space-2 space-top-xl-3 space-bottom-lg-3">
          <div className="w-md-80 w-lg-50 text-center mx-md-auto mb-5 mb-md-9">
            <h2>Why Garden Academy</h2>
            <p>
              Pellentesque donec ut accumsan nibh turpis massa facilisis
              pellentesque amet.
            </p>
          </div>

          <div className="row mx-n2 mx-lg-n3">
            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
              <div className="card">
                <div className="card-icon">
                  <span className="span-icon">
                    {" "}
                    <img src={homecard1} alt="" />{" "}
                  </span>
                </div>
                <h3 className="card-headers">Top Industry Facilitators</h3>
                <div className="card-body">
                  Learn from Subject matter experts from different areas of the
                  tech industry and gain the knowledge you need to rise to the
                  top of your field.{" "}
                </div>
              </div>
            </div>
            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
              <div className="card">
                <div className="card-icon">
                  <span className="span-icon">
                    {" "}
                    <img src={homecard2} alt="" />{" "}
                  </span>
                </div>
                <h3 className="card-headers">Resume & Interview Prep</h3>
                <div className="card-body">
                  Gain valuable tips and hacks you need to create an appealing
                  resume and navigate interview scenarios.{" "}
                </div>
              </div>
            </div>

            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
              <div className="card">
                <div className="card-icon">
                  <span className="span-icon">
                    {" "}
                    <img src={homecard3} alt="" />{" "}
                  </span>
                </div>
                <h3 className="card-headers">Flexible Learning</h3>
                <div className="card-body">
                  Learn wherever, whenever with quality content delivered to
                  your device on demand. Powered by VigiLearnLMS™.
                </div>
              </div>
            </div>
            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
              <div className="card">
                <div className="card-icon">
                  <span className="span-icon">
                    {" "}
                    <img src={homecard4} alt="" />{" "}
                  </span>
                </div>
                <h3 className="card-headers">Flexible Payment Options</h3>
                <div className="card-body">
                  Take advantage of any of our available fee payment options and
                  enjoy unrivalled ease of access.{" "}
                </div>
              </div>
            </div>
            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
              <div className="card">
                <div className="card-icon">
                  <span className="span-icon">
                    {" "}
                    <img src={homecard5} alt="" />{" "}
                  </span>
                </div>
                <h3 className="card-headers">
                  Globally Recognized <br /> Certificate
                </h3>
                <div className="card-body">
                  Receive a certificate of international repute upon completion
                  of your chosen learning path.
                </div>
              </div>

              {/* <!-- End Card --> */}
            </div>
            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
              {/* <!-- Card --> */}
              <div className="card">
                <div className="card-icon">
                  <span className="span-icon">
                    {" "}
                    <img src={homecard6} alt="" />{" "}
                  </span>
                </div>
                <h3 className="card-headers">
                  Internship & Full-time <br /> opportunities
                </h3>
                <div className="card-body">
                  Put your newly acquired skills to use with access to work
                  opportunities across the global tech industry.
                </div>
              </div>
            </div>
          </div>
        </div>

        <div className=" d-lg-flex position-relative session-three">
          <div className=" container d-lg-flex align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
            <section className=" ">
              <div className="img2-container mb-4">
                <img className="img-fluid img2-style" src={homehero2} alt="" />
              </div>
              <div className="container">
                <h1 className="font-weight-bold white-text">
                  Looking To Improve Your Workforce?
                </h1>
                <p
                  style={{
                    marginTop: "20px",
                    textAlign: "left",
                    fontSize: "18px",
                  }}
                >
                  The terrain of the global tech industry is rapidly evolving,
                  and it is imperative that your workforce stays empowered and
                  relevant in today’s world. By exposing your personnel to
                  quality learning opportunities on Garden Academy, their
                  horizons are broadened, and they become empowered to compete
                  on the global playing field while contributing their newly
                  acquired skills to the growth of your organization.
                </p>

                <div className="row" style={{ marginTop: "70px" }}>
                  <div className="col-md-4 mb-3" style={{ padding: "0px" }}>
                    <div className="card-icon">
                      <span>
                        <img src={rectangle} alt="" />
                      </span>
                    </div>
                    <h3 className="card-headers white-text">Hire Talent</h3>
                    <div className="card-body s3-para">
                      Garden Academy harnesses the knowledge & experience of
                      global industry experts and channels it into truly
                      expansive courses that create the best talent who are
                      primed and ready to take your organization to the next
                      level.{" "}
                    </div>
                  </div>
                  <div className="col-md-4 mb-3">
                    <div className="card-icon">
                      <span>
                        <img src={rectangle} alt="" />{" "}
                      </span>
                    </div>
                    <h3 className="card-headers white-text">
                      Upskill Existing Staff
                    </h3>
                    <div className="card-body s3-para">
                      With the best facilitators delivering quality content,
                      Garden Academy vastly improves the skill level of staff
                      groups. Your staff will gain the skills needed to improve
                      business outcomes and contribute directly to your
                      organization’s bottom line{" "}
                    </div>
                  </div>
                  <div className="col-md-4 mb-3">
                    <div className="card-icon">
                      <span>
                        <img src={rectangle} alt="" />{" "}
                      </span>
                    </div>
                    <h3 className="card-headers white-text">
                      Staff Onboarding
                    </h3>
                    <div className="card-body s3-para">
                      With the best facilitators delivering quality content,
                      Garden Academy vastly improves the skill level of staff
                      groups. Your staff will gain the skills needed to improve
                      business outcomes and contribute directly to your
                      organization’s bottom line{" "}
                    </div>
                  </div>
                </div>

                <div className="get-started">
                  <button className="btn get-started-btn">Get Started</button>
                </div>
              </div>
            </section>
          </div>
        </div>

        {programs?.length > 0 && (
          <div className="session-four container space-2 space-top-xl-3 space-bottom-lg-3">
            <div className="w-md-80 text-center mx-md-auto mb-5 mb-md-9">
              <h2>Available Programs</h2>
              <p>
                Select any program from our library of carefully crafted
                programs guaranted to take you{" "}
              </p>
            </div>
            <section>
              <div className="row mx-n2 mx-lg-n3">
                <CourseCardGridView
                  grid={4}
                  programs={programs.slice(0, 3)}
                ></CourseCardGridView>
              </div>
            </section>

            <div className="get-started">
              <Link
                to={{
                  pathname: "/programs",
                  state: {
                    data: programs.slice(0, 3),
                  },
                }}
                className="btn programs-btn"
              >
                <b>View All Programs</b>
              </Link>
            </div>
          </div>
        )}

        <div className="session-five d-lg-flex align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
          <div className="row space-bottom-2" style={{ margin: "1% 5%" }}>
            <div className="col-lg-5 mt-5">
              <hr
                className="mt-5"
                style={{
                  width: "10%",
                  border: "2px solid #0B2253",
                  opacity: "0.5",
                  margin: "0px",
                }}
              />
              <h3
                className="mt-3"
                style={{ fontSize: "36px", color: "#041644" }}
              >
                Become a Facilator
              </h3>
              <p className="mt-3 facillator-p">
                Lorem ipsum dolor sit amet, consectetur vred adipiscing tortor,
                pellentesque donec deaut accumsan nibh turpis eu massa
                consectetur adipiscing tortor benelit.
              </p>
              <br />
              <p className="facillator-p">
                Lorem ipsum dolor sit amet, consectetur vred adipiscing
                adipiscing tortor, pellentesque donec deaut accumsan nibh turpis
                pellentesque donec deaut consectetur.
              </p>
              <br />
              <br />
              <button onClick={handleShow} className="btn btn-lg  btn-primary">
                Become a Facillator
              </button>
            </div>

            <div className="col-lg-7 col-xs-12 mt-5">
              <img
                className="img-fluid d-lg-block"
                src={facilitator}
                alt="facillator"
              />
            </div>
          </div>
        </div>

        {images.length > 0 && (
          <div className=" container d-lg-flex  align-items-lg-center space-top-2 space-lg-3 space-lg-0 min-vh-lg-100">
            <div className="row">
              <div
                className="col-lg-4 mt-5"
                style={{
                  display: "flex",
                  justifyContent: "space-between",
                  alignItems: "center",
                  flexDirection: "column",
                }}
              >
                <div>
                  <h4 className="mt-3 testimonials-heading">Testimonials</h4>
                  <p className="mt-3" style={{ fontSize: "35px" }}>
                    Read what our users have to say...
                  </p>
                </div>

                <div
                  style={{
                    display: "flex",
                    justifyContent: "space-around",
                    alignItems: "center",
                    width: "100%",
                  }}
                >
                  <span
                    className="fa fa-arrow-left slick-arrow slick-arrow-primary-white slick-arrow-left shadow-soft rounded-circle ml-sm-n2"
                    onClick={slideLeft}
                    style={{
                      background: index === 0 ? "#1354D3" : "",
                      color: index === 0 ? "#fff" : "",
                    }}
                  ></span>
                  <h4>
                    {index + 1} / {images.length}
                  </h4>
                  <span
                    className="fa fa-arrow-right slick-arrow slick-arrow-primary-white slick-arrow-right shadow-soft rounded-circle mr-sm-2 mr-xl-4"
                    onClick={slideRight}
                    style={{
                      background: index === images.length - 1 ? "#1354D3" : "",
                      color: index === images.length - 1 ? "#fff" : "",
                    }}
                  ></span>
                </div>
              </div>

              <div className="col-lg-8 mt-5">
                <div className="row">
                  <div className="col-md-6 col-sm-12 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                    <div
                      className="card  shadow pt-3 pb-5 px-2"
                      style={{ height: "auto" }}
                    >
                      <div
                        className="row card-icon"
                        style={{ marginBottom: "0 !important" }}
                      >
                        <div className="col-3">
                          <img
                            className="avatar img-fluid"
                            src={images[index].image}
                            alt="avatar"
                          />
                        </div>
                        <div className="col-9">
                          <p style={{ fontSize: "24px", margin: "0px" }}>
                            {images[index].title}
                          </p>
                          <p style={{ fontSize: "18px", color: "#81909D" }}>
                            {images[index].social}
                          </p>
                        </div>
                      </div>

                      <div className="card-body" style={{ color: "#3A434B" }}>
                        <hr />
                        <br />
                        <p>{images[index].content}</p>
                      </div>
                    </div>
                  </div>
                  {images.length - 1 >= index + 1 && (
                    <div className="d-lg-block d-md-block d-none col-md-6 col-sm-12 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                      <div
                        className="card  shadow pt-3 pb-5 px-2"
                        style={{ height: "auto" }}
                      >
                        <div
                          className="row card-icon"
                          style={{ marginBottom: "0 !important" }}
                        >
                          <div className="col-3">
                            <img
                              className="avatar img-fluid"
                              src={images[index + 1].image}
                              alt="avatar"
                            />
                          </div>
                          <div className="col-9">
                            <p style={{ fontSize: "24px", margin: "0px" }}>
                              {images[index + 1].title}
                            </p>
                            <p style={{ fontSize: "18px", color: "#81909D" }}>
                              {images[index + 1].social}
                            </p>
                          </div>
                        </div>

                        <div className="card-body" style={{ color: "#3A434B" }}>
                          <hr />
                          <br />
                          <p>{images[index + 1].content}</p>
                        </div>
                      </div>
                    </div>
                  )}
                </div>
              </div>
            </div>
          </div>
        )}
      </main>

      <Modal
        size="lg"
        aria-labelledby="contained-modal-title-vcenter"
        centered
        show={showModal}
        onHide={handleClose}
      >
        <Modal.Header closeButton>
          <Modal.Title id="contained-modal-title-vcenter">
            Reserve a spot
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form>
            <div className="row">
              <div className="col-sm-6">
                <div className="js-form-message form-group">
                  <label htmlFor="firstName" className="input-label">
                    First name
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    name="firstName"
                    id="firstName"
                    placeholder="eg. Nataly"
                    required
                    value={firstName}
                    onChange={(e) => setFirstName(e.target.value)}
                    onBlur={(e) => firstNameValidation(e.target.value)}
                  />
                  <p className="text-danger">{firstNameValid}</p>
                </div>
              </div>

              <div className="col-sm-6">
                <div className="js-form-message form-group">
                  <label htmlFor={"lastName"} className="input-label">
                    Last name
                  </label>
                  <input
                    type="text"
                    className="form-control"
                    name="lastName"
                    id="lastName"
                    placeholder="eg. Gaga"
                    required
                    value={lastName}
                    onChange={(e) => setLastName(e.target.value)}
                    onBlur={(e) => lastNameValidation(e.target.value)}
                  />
                  <p className="text-danger">{lastNameValid}</p>
                </div>
              </div>

              <div className="col-sm-6">
                <div className="js-form-message form-group">
                  <label htmlFor={"firstName"} className="input-label">
                    Phone Number
                  </label>
                  <input
                    type="tel"
                    className="form-control"
                    name="firstName"
                    id="firstName"
                    placeholder="08045275625"
                    required
                    value={phone}
                    onChange={(e) => setPhone(e.target.value)}
                    onBlur={(e) => phoneValidation(e.target.value)}
                  />
                  <p className="text-danger">{phoneValid}</p>
                </div>
              </div>

              <div className="col-sm-6">
                <div className="js-form-message form-group">
                  <label htmlFor={"lastName"} className="input-label">
                    Email Address
                  </label>
                  <input
                    type="email"
                    className="form-control"
                    name="email"
                    id="email"
                    placeholder="admin@gmail.com"
                    required
                    value={email}
                    onChange={(e) => setEmail(e.target.value)}
                    onBlur={(e) => emailValidation(e.target.value)}
                  />
                  <p className="text-danger">{emailValid}</p>
                </div>
              </div>

              <div className="col-sm-12">
                <div className="js-form-message form-group">
                  <label htmlFor={"lastName"} className="input-label">
                    Portfolio Link
                  </label>
                  <input
                    type="url"
                    className="form-control"
                    name="url"
                    id="url"
                    placeholder="https://drive.google.com/jfjfjfjfjjffff"
                    required
                    value={url}
                    onChange={(e) => setUrl(e.target.value)}
                    onBlur={(e) => urlValidation(e.target.value)}
                  />
                  <p className="text-danger">{urlValid}</p>
                </div>
              </div>
            </div>
          </form>
        </Modal.Body>
        <Modal.Footer>
          <Button variant="secondary" onClick={handleClose}>
            Close
          </Button>
          <Button variant="primary" onClick={handleSubmit}>
            Next
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
};

export default AppHomePage;
