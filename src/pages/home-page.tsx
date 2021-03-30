import * as React from "react";
import { Link } from "react-router-dom";
import { Modal, Button, Alert } from "react-bootstrap";
import { useState, useEffect } from "react";
// @ts-ignore
import Fade from "react-reveal/Fade";
// @ts-ignore
import Zoom from "react-reveal/Zoom";

import "../assets/css/homepage.css";
import { postMethods, getMethods, getCourses } from "../helpers/api";
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
  history: any;
}

const AppHomePage: React.SFC<AppHomePageProps> = ({ history }) => {
  const searchCourse = (searchInput: string) => {
    if (searchInput.trim() === "") return;
    history.push({
      pathname: "/programs",
      state: {
        searchInput,
        data: programs.filter((course: any) => {
          return course.name.toUpperCase().includes(searchInput.toUpperCase());
        }),
      },
    });
  };

  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    phone: "",
    email: "",
    url: "https://",
    firstNameValid: "",
    lastNameValid: "",
    phoneValid: "",
    emailValid: "",
    urlValid: "",
  });

  const [showModal, setShowModal] = useState(false);
  const [programs, setPrograms] = useState([]);

  const [showAlert, setShowAlert] = useState({
    text: "",
    show: false,
    color: "success",
  });

  const [buttonText, setButtonText] = useState({
    text: "Submit",
    disabled: false,
  });

  const [images] = useState([
    {
      image: person1,
      title: "Dimeji Adeojo",
      social: "Product Designer",
      content:
        "With Garden Academy, I have been able to upscale my skills as a Product Designer. I got introduced to new ways and techniques to designing products that is customer centric and focused on giving the customer a good experience as they move further down the product funnel",
    },
    {
      image: person2,
      title: "Chibuzor Okoro",
      social: "FrontEnd Intern",
      content:
        "As a graduate of Economics, I have always been interested in the world of tech, which lead my passion to learning how to code as a frontend developer. Getting into Garden Academy, I was introduced to the programming languages (HTML, CSS, JavaScript). With the few months spent in my learning, I was ble to secure an internship as a Front-end developer in a fintech",
    },

    {
      image: person1,
      title: "Silva Folabi",
      social: "Freelance Developer",
      content:
        "After 4 years in the banking industry, I realized that my passion wasn’t in banking but in tech, because all through my University days I was a tech enthusiast. But I couldn’t move into the world of tech with a Bachelor’s degree in Business Admin. I needed the skills to function. Then I got to know about Garden Academy, and with the few months learning to be a backend developer, I have been able to code my own website, and also doing some freelance projects",
    },
    {
      image: person2,
      title: "Tolu Odewole",
      social: "HR Manager",
      content:
        "Garden Academy is the best platform to learn and upgrade your skills in tech. As a HR professional, I can say that Garden Academy has impacted me so much that I can go back to my workplace and function better",
    },
    {
      image: person2,
      title: "Gerard Obi",
      social: "Business Analyst",
      content:
        "I have been on the look-out for courses to take to improve myself as a Business Analyst, and getting to know about Garden Academy was the breakthrough I needed. The tutors are well experienced with lots of accolades on their belts, and we got to breakdown real-life business issues, and working to find solutions to them.",
    },
    {
      image: person2,
      title: "Aliyu, Bamako",
      social: "Project Manager",
      content:
        "Garden Academy is a top class platform for me. As a student of the academy, I enrolled for its project management course, and I didn’t regret my decision, because I got to understand what it is expected of me as a project manager. Also, the support service was excellent",
    },
    {
      image: person2,
      title: "Jeremiah Sunday",
      social: "Lawyer",
      content:
        "Being a law graduate and fresh out of Law School, I needed to do something while waiting to be posted for NYSC, then I got to know about Garden Academy through a friend. I took up their law course and after finishing the course, I got an offer to work as a Junior Associate in for Tech start-up",
    },
    {
      image: person2,
      title: "Tobe Anwuli",
      social: "Frontend Developer",
      content:
        "With over 3 years of experience as a Frontend developer, I needed an advance course to push me to the next stage of my career. I saw the ad about Garden Academy on social media and decided to opt-in for a course in Frontend development. It was indeed what I was looking for, and the tutors were very experienced and understanding. And with a certification from Garden Academy, I secured a promotion at my workplace.",
    },
    {
      image: person2,
      title: "Nonso Okpala",
      social: "Business Manager",
      content:
        "One thing I am proud of as a graduate of Garden Academy is that I have been able to showcase my leadership skills much better at a management level. With its leadership course, I have been able to work on myself as a leader.",
    },
    {
      image: person2,
      title: "Bunmi Ogunye",
      social: "Data Analyst",
      content:
        "Despite graduating as a Pharmacist, I needed the skill to analyze data and also to secure a job as a Data Analyst, because I wanted to focus more on the research aspect of Pharmacy. Garden Academy provided for me the necessary skills I needed, and after completion, I secured an employment as a Data Analyst for a Pharmaceutical company.",
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

  const handleSubmit = async () => {
    if (firstNameValidation(form.firstName) !== true) {
      return firstNameValidation(form.firstName);
    }

    if (lastNameValidation(form.lastName) !== true) {
      return lastNameValidation(form.lastName);
    }

    if (phoneValidation(form.phone) !== true) {
      return phoneValidation(form.phone);
    }

    if (emailValidation(form.email) !== true) {
      return emailValidation(form.email);
    }

    if (urlValidation(form.url) !== true) {
      return urlValidation(form.url);
    }

    setButtonText({
      text: "Loading ...",
      disabled: true,
    });

    const facillatorData = await postMethods("/Edulearn/facilitator", {
      firstName: form.firstName,
      lastName: form.lastName,
      email: form.email,
      phoneNumber: form.phone,
    });

    setButtonText({
      text: "Submit",
      disabled: false,
    });

    if (facillatorData.requestSuccessful) {
      setShowAlert({
        text: "Facilitator request was submitted successfully",
        show: true,
        color: "success",
      });
      setTimeout(() => {
        setShowAlert({
          text: "",
          show: false,
          color: "primary",
        });
        handleClose();
      }, 5000);

      setForm({
        firstName: "",
        lastName: "",
        phone: "",
        email: "",
        url: "https://",
        firstNameValid: "",
        lastNameValid: "",
        phoneValid: "",
        emailValid: "",
        urlValid: "",
      });
    } else {
      setShowAlert({
        text: facillatorData.message,
        show: true,
        color: "danger",
      });
    }
  };

  useEffect(() => {
    const fetchPrograms = async () => {
      const tokenData = await getMethods("/Edulearn/token");

      if (tokenData.status) {
        const loginData = await getCourses(tokenData.token);

        if (loginData.status) {
          setPrograms(loginData.data);
        }
      }
    };

    fetchPrograms();
  }, []);

  const firstNameValidation = (fieldValue: string): boolean => {
    if (fieldValue.trim() === "") {
      setForm({
        ...form,
        firstNameValid: "First name is required",
      });
      return false;
    }

    if (/[^a-zA-Z -]/.test(fieldValue)) {
      setForm({
        ...form,
        firstNameValid: "Invalid characters",
      });

      return false;
    }

    if (fieldValue.trim().length < 3) {
      setForm({
        ...form,
        firstNameValid: "First name needs to be at least three characters",
      });

      return false;
    }

    setForm({
      ...form,
      firstNameValid: "",
    });

    return true;
  };

  const lastNameValidation = (fieldValue: string): boolean => {
    if (fieldValue.trim() === "") {
      setForm({
        ...form,
        lastNameValid: "Last name is required",
      });
      return false;
    }

    if (/[^a-zA-Z -]/.test(fieldValue)) {
      setForm({
        ...form,
        lastNameValid: "Invalid characters",
      });
      return false;
    }

    if (fieldValue.trim().length < 3) {
      setForm({
        ...form,
        lastNameValid: "Last name needs to be at least three characters",
      });
      return false;
    }
    setForm({
      ...form,
      lastNameValid: "",
    });

    return true;
  };

  const emailValidation = (email: string): boolean => {
    if (
      /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(
        email
      )
    ) {
      setForm({
        ...form,
        emailValid: "",
      });
      return true;
    }
    if (email.trim() === "") {
      setForm({
        ...form,
        emailValid: "Email is required",
      });

      return false;
    }
    setForm({
      ...form,
      emailValid: "Please enter a valid email",
    });
    return false;
  };

  const phoneValidation = (phone: string): boolean => {
    if (/^[0]\d{10}$/.test(phone)) {
      setForm({
        ...form,
        phoneValid: "",
      });
      return true;
    }
    if (phone.trim() === "") {
      setForm({
        ...form,
        phoneValid: "Phone is required",
      });
      return false;
    }
    setForm({
      ...form,
      phoneValid: "Please enter a valid phone",
    });
    return false;
  };

  const urlValidation = (url: string): boolean => {
    if (
      /((([A-Za-z]{3,9}:(?:\/\/)?)(?:[-;:&=\+\$,\w]+@)?[A-Za-z0-9.-]+(:[0-9]+)?|(?:www.|[-;:&=\+\$,\w]+@)[A-Za-z0-9.-]+)((?:\/[\+~%\/.\w-_]*)?\??(?:[-\+=&;%@.\w_]*)#?(?:[\w]*))?)/.test(
        url
      )
    ) {
      setForm({
        ...form,
        urlValid: "",
      });
      return true;
    }
    if (url.trim() === "") {
      setForm({
        ...form,
        urlValid: "Portfolio URL is required",
      });
      return false;
    }
    setForm({
      ...form,
      urlValid: "Please enter a valid URL",
    });
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
                    <h1 className="display-4 mb-3 animated slideInDown">
                      Learn the <span className="text-primary">skills</span> you
                      need to <span className="text-primary">succeed</span>
                      {/* <span className="text-primary text-highlight-warning">
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
                      </span> */}
                    </h1>
                    <p className="lead  animated slideInUp">
                      We are committed to training the next generation of tech
                      superstars and help organisations upscale their workforce
                      with the right talent
                    </p>
                  </div>

                  <Search
                    search={"What do you want to learn"}
                    button_text={"Search"}
                    onSearchSubmit={searchCourse}
                    searchData=""
                  ></Search>
                  <br />
                  <br />
                </div>

                <div className="col-lg-6 ">
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
        <Fade left>
          <div className="container space-2 space-top-xl-3 space-bottom-lg-3">
            <div className="w-md-80 w-lg-60 text-center mx-md-auto mb-5 mb-md-9">
              <h2>Why Garden Academy</h2>
              <p>
                By becoming a Garden Academy alumnus, you gain immediate access
                to a variety of benefits.
              </p>
            </div>

            <div className="row mx-n2 mx-lg-n3">
              <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                <div className="card card-hover">
                  <div className="card-icon">
                    <span className="span-icon">
                      <img src={homecard1} alt="" />
                    </span>
                  </div>
                  <h3 className="card-headers">Top Industry Facilitators</h3>
                  <div className="card-body">
                    Learn from Subject matter experts from different areas of
                    the tech industry and gain the knowledge you need to rise to
                    the top of your field.
                  </div>
                </div>
              </div>
              <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                <div className="card card-hover">
                  <div className="card-icon">
                    <span className="span-icon">
                      <img src={homecard2} alt="" />
                    </span>
                  </div>
                  <h3 className="card-headers">Resume & Interview Prep</h3>
                  <div className="card-body">
                    Gain valuable tips and hacks you need to create an appealing
                    resume and navigate interview scenarios.
                  </div>
                </div>
              </div>

              <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                <div className="card card-hover">
                  <div className="card-icon">
                    <span className="span-icon">
                      <img src={homecard3} alt="" />
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
                <div className="card card-hover">
                  <div className="card-icon">
                    <span className="span-icon">
                      <img src={homecard4} alt="" />
                    </span>
                  </div>
                  <h3 className="card-headers">Flexible Payment Options</h3>
                  <div className="card-body">
                    Take advantage of any of our available fee payment options
                    and enjoy unrivalled ease of access.
                  </div>
                </div>
              </div>
              <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                <div className="card card-hover">
                  <div className="card-icon">
                    <span className="span-icon">
                      <img src={homecard5} alt="" />
                    </span>
                  </div>
                  <h3 className="card-headers">
                    Globally Recognized <br /> Certificate
                  </h3>
                  <div className="card-body">
                    Receive a certificate of international repute upon
                    completion of your chosen learning path.
                  </div>
                </div>

                {/* <!-- End Card --> */}
              </div>
              <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                {/* <!-- Card --> */}
                <div className="card card-hover">
                  <div className="card-icon">
                    <span className="span-icon">
                      <img src={homecard6} alt="" />
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
        </Fade>
        <Fade right>
          {programs?.length > 0 && (
            <div style={{ background: "#E8EFFD", width: "100%" }}>
              <div className="session-four container space-2 space-top-xl-3 space-bottom-lg-3">
                <div className="w-md-80 text-center mx-md-auto mb-5 mb-md-9">
                  <h2>Available Programs</h2>
                  <p>
                    Select any program from our library of carefully crafted
                    programs guaranted to take you
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
                        data: programs,
                      },
                    }}
                    className="btn programs-btn btn-hover"
                  >
                    <b>View All Programs</b>
                  </Link>
                </div>
              </div>
            </div>
          )}
        </Fade>
        <div style={{ background: "#E8EFFD", width: "100%" }}>
          <div className=" d-lg-flex position-relative session-three">
            <div className=" container d-lg-flex align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
              <section className=" ">
                <div className="img2-container mb-4">
                  <img
                    className="img-fluid img2-style"
                    src={homehero2}
                    alt=""
                  />
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
                    <div className="col-md-4 mb-3">
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
                        level.
                      </div>
                    </div>
                    <div className="col-md-4 mb-3">
                      <div className="card-icon">
                        <span>
                          <img src={rectangle} alt="" />
                        </span>
                      </div>
                      <h3 className="card-headers white-text">
                        Upskill Existing Staff
                      </h3>
                      <div className="card-body s3-para">
                        With the best facilitators delivering quality content,
                        Garden Academy vastly improves the skill level of staff
                        groups. Your staff will gain the skills needed to
                        improve business outcomes and contribute directly to
                        your organization’s bottom line
                      </div>
                    </div>
                    <div className="col-md-4 mb-3">
                      <div className="card-icon">
                        <span>
                          <img src={rectangle} alt="" />
                        </span>
                      </div>
                      <h3 className="card-headers white-text">
                        Staff Onboarding
                      </h3>
                      <div className="card-body s3-para">
                        With the best facilitators delivering quality content,
                        Garden Academy vastly improves the skill level of staff
                        groups. Your staff will gain the skills needed to
                        improve business outcomes and contribute directly to
                        your organization’s bottom line
                      </div>
                    </div>
                  </div>

                  <div className="get-started">
                    <Link
                      to="/employers"
                      className="btn get-started-btn btn-hover"
                    >
                      Get Started
                    </Link>
                  </div>
                </div>
              </section>
            </div>
          </div>
        </div>
        <Fade left>
          <div className="session-five d-lg-flex align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
            <div className="container">
              <div className="row space-bottom-2">
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
                    Become a Facilitator
                  </h3>
                  <p className="mt-3 facillator-p">
                    At any learning establishment, top-notch faculty are crucial
                    to the success of that operation. At Garden Academy, the
                    door is always open for facilitators who are experts in
                    their chosen fields, and use engaging and interactive
                    learner-focused approaches to teach and transfer practical
                    skills
                  </p>
                  <br />
                  <p className="facillator-p">
                    Do you have what it takes to train the next crop of tech
                    superstars? Get in touch with us.
                  </p>
                  <br />
                  <br />
                  <button
                    onClick={handleShow}
                    className="btn btn-lg  btn-primary btn-hover"
                    style={{
                      padding: "16px 32px",
                      borderRadius: "4px",
                      background: "#0F42A4",
                    }}
                  >
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
          </div>
        </Fade>
        <Zoom>
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
                        background:
                          index === images.length - 1 ? "#1354D3" : "",
                        color: index === images.length - 1 ? "#fff" : "",
                      }}
                    ></span>
                  </div>
                </div>

                <div className="col-lg-8 mt-5">
                  <div className="row">
                    <div className="col-md-6 col-sm-12 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                      <div
                        className="card card-hover  shadow pt-3 pb-5 px-2"
                        style={{
                          boxShadow:
                            "0px 4px 4px rgb(135 146 161 / 16%), 0px 6px 41px rgb(135 146 161 / 11%) !important",
                          height: "auto",
                          border: "none",
                        }}
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
                            <h4 style={{ margin: "0px" }}>
                              {images[index].title}
                            </h4>
                            <p style={{ color: "#81909D" }}>
                              {images[index].social}
                            </p>
                          </div>
                        </div>

                        <div className="card-body" style={{ color: "#3A434B" }}>
                          <hr />
                          <br />
                          <p className="tip">
                            <span>{images[index].content}</span>
                            {images[index].content.length > 200
                              ? images[index].content.substring(0, 200) + "..."
                              : images[index].content}
                          </p>
                        </div>
                      </div>
                    </div>
                    {images.length - 1 >= index + 1 && (
                      <div className="d-lg-block d-md-block d-none col-md-6 col-sm-12 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                        <div
                          className="card card-hover shadow pt-3 pb-5 px-2"
                          style={{
                            boxShadow:
                              "0px 4px 4px rgb(135 146 161 / 16%), 0px 6px 41px rgb(135 146 161 / 11%) !important",
                            height: "auto",
                            border: "none",
                          }}
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
                              <h4 style={{ margin: "0px" }}>
                                {images[index + 1].title}
                              </h4>
                              <p style={{ color: "#81909D" }}>
                                {images[index + 1].social}
                              </p>
                            </div>
                          </div>

                          <div
                            className="card-body"
                            style={{ color: "#3A434B" }}
                          >
                            <hr />
                            <br />
                            <p className="tip">
                              <span>{images[index + 1].content}</span>
                              {images[index + 1].content.length > 200
                                ? images[index + 1].content.substring(0, 200) +
                                  "..."
                                : images[index + 1].content}
                            </p>
                          </div>
                        </div>
                      </div>
                    )}
                  </div>
                </div>
              </div>
            </div>
          )}
        </Zoom>
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
            Become a facilitator
          </Modal.Title>
        </Modal.Header>
        <Modal.Body>
          <form>
            <div className="row">
              <div className="offset-2 col-sm-8">
                <Alert
                  show={showAlert.show}
                  variant={showAlert.color}
                  onClose={() => setShowAlert({ ...showAlert, show: false })}
                  dismissible
                >
                  <Alert.Heading className="text-light">
                    {showAlert.text}
                  </Alert.Heading>
                </Alert>
              </div>
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
                    value={form.firstName}
                    onChange={(e) => {
                      setForm({ ...form, firstName: e.target.value });
                    }}
                    onBlur={(e) => {
                      firstNameValidation(e.target.value);
                    }}
                  />
                  <p className="text-danger">{form.firstNameValid}</p>
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
                    value={form.lastName}
                    onChange={(e) => {
                      setForm({ ...form, lastName: e.target.value });
                    }}
                    onBlur={(e) => {
                      lastNameValidation(e.target.value);
                    }}
                  />
                  <p className="text-danger">{form.lastNameValid}</p>
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
                    value={form.phone}
                    onChange={(e) => {
                      setForm({ ...form, phone: e.target.value });
                    }}
                    onBlur={(e) => {
                      phoneValidation(e.target.value);
                    }}
                  />
                  <p className="text-danger">{form.phoneValid}</p>
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
                    value={form.email}
                    onChange={(e) => {
                      setForm({ ...form, email: e.target.value });
                    }}
                    onBlur={(e) => {
                      emailValidation(e.target.value);
                    }}
                  />
                  <p className="text-danger">{form.emailValid}</p>
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
                    value={form.url}
                    onChange={(e) => {
                      setForm({ ...form, url: e.target.value });
                    }}
                    onBlur={(e) => {
                      urlValidation(e.target.value);
                    }}
                  />
                  <p className="text-danger">{form.urlValid}</p>
                </div>
              </div>
            </div>
          </form>
        </Modal.Body>
        <Modal.Footer>
          <Button
            variant="secondary"
            onClick={handleClose}
            className="btn-hover"
          >
            Close
          </Button>
          <Button
            disabled={buttonText.disabled}
            variant="primary"
            onClick={handleSubmit}
            className="btn-hover"
          >
            {buttonText.text}
          </Button>
        </Modal.Footer>
      </Modal>
    </>
  );
};

export default AppHomePage;
