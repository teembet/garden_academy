import { Modal, Button } from "react-bootstrap";
import { useState } from "react";

import homehero from "../assets/img/home-hero.svg";
import womanPic from "../assets/img/woman.svg";
import homecard1 from "../assets/img/homecard1.svg";
import homecard2 from "../assets/img/homecard2.svg";
import homecard3 from "../assets/img/homecard3.svg";
import facilitator from "../assets/img/facilitator.svg";
import employer from "../assets/img/employer.svg";
import classP from "../assets/img/class.svg";

export interface AppForEmployersProps {}

const AppForEmployers: React.SFC<AppForEmployersProps> = () => {
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
          style={{ paddingBottom: "5%", backgroundColor: "#EAFBF1" }}
        >
          <div className="container d-lg-flex  align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
            <div className="w-md-100">
              <div className="row">
                <div className="col-lg-6">
                  <div className="mb-5 mt-11">
                    <h1 className="display-4 mb-3 text-primary">
                      Hire Top Tech Talent.Upskill Your Existing Workforce.
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
                      With the best minds being developed by our team of
                      industry experts, Garden Academy connects employers to a
                      pool of top talent across multiple fields.
                    </p>
                    <p className="lead">
                      Are you ready to take your organization to the next level?
                    </p>
                    <br />
                    <br />
                    <div className="mt-3">
                      <button className="btn btn-md  btn-primary">
                        Get Started
                      </button>
                    </div>
                  </div>

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
                  {/* <br className="d-lg-none d-md-none" />
                                    <br className="d-lg-none d-md-none" /> */}
                </div>
              </div>
            </div>
            {/* <!-- End Content -->

       
          {/* <!-- End SVG Shape --> */}
          </div>
        </div>
        <div className="container space-2 space-top-xl-3 space-bottom-lg-3">
          {/* <!-- Title --> */}
          <div className="w-md-80 w-lg-50 text-center mx-md-auto mb-5 mb-md-9">
            <h2>What Can We Do For You?</h2>
            <p>
              Garden Academy is uniquely poised to tackle your personnel-related
              challenges
            </p>
          </div>
          {/* <!-- End Title --> */}

          <div className="row mx-n2 mx-lg-n3">
            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
              {/* <!-- Card --> */}
              <div
                className="card"
                style={{
                  height: "27rem",
                }}
              >
                <div className="card-icon">
                  <span className="span-icon">
                    <img src={homecard1} alt="" />
                  </span>
                </div>
                <h3 className="card-headers">Hire Talent</h3>
                <div className="card-body">
                  Garden Academy harnesses the knowledge & experience of global
                  industry experts and channels it into truly expansive courses
                  that create the best talent who are primed and ready to take
                  your organization to the next level.
                </div>
              </div>

              {/* <!-- End Card --> */}
            </div>
            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
              {/* <!-- Card --> */}
              <div
                className="card"
                style={{
                  height: "27rem",
                }}
              >
                <div className="card-icon">
                  <span className="span-icon">
                    <img src={homecard2} alt="" />
                  </span>
                </div>
                <h3 className="card-headers">Upskill existing Staff</h3>
                <div className="card-body">
                  With the best facilitators delivering quality content, Garden
                  Academy vastly improves the skill level of staff groups. Your
                  staff will gain the skills needed to improve business outcomes
                  and contribute directly to your organization’s bottom line.
                </div>
              </div>

              {/* <!-- End Card --> */}
            </div>

            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
              {/* <!-- Card --> */}
              <div
                className="card"
                style={{
                  height: "27rem",
                }}
              >
                <div className="card-icon">
                  <span className="span-icon">
                    <img src={homecard3} alt="" />
                  </span>
                </div>
                <h3 className="card-headers">Staff Onboarding</h3>
                <div className="card-body">
                  To simplify your organization’s staff onboarding process, the
                  Garden Academy team is always available to manage all
                  activities from orientation to training and staff assessment.
                  With our help, your new staff can be seamlessly integrated
                  into your organization and be positioned to deliver the best
                  results.
                </div>
              </div>

              {/* <!-- End Card --> */}
            </div>
          </div>
        </div>
        <div
          className=" session-five d-lg-flex align-items-lg-center space-2 space-top-xl-3 space-bottom-lg-3 space-top-2 space-lg-0 min-vh-lg-100"
          style={{ flexDirection: "column", backgroundColor: "#E9FAFB" }}
        >
          <div className="w-md-80 w-lg-50 text-center mx-md-auto mb-5 mb-md-9">
            <h2 style={{ fontSize: "36px" }}>What You Will Get</h2>
            <p>
              Garden Academy exposes your organisation to a universal benefit
              that translates into better business outcome in the near future
            </p>
          </div>
          <div className="row space-bottom-2" style={{ margin: "1% 5%" }}>
            <div className="col-lg-5 mt-5">
              <div>
                <h2 className="text-primary" style={{ fontSize: "24px" }}>
                  Quality People
                </h2>
                <p>
                  Few would argue that, despite the advancements of feminism
                  over the past three decades, women still face a double
                  standard when it comes to their behavior.
                </p>
              </div>
              <div className="mt-2">
                <h2 className="text-primary" style={{ fontSize: "24px" }}>
                  Cost Effective Scaling
                </h2>
                <p>
                  Few would argue that, despite the advancements of feminism
                  over the past three decades, women still face a double
                  standard when it comes to their behavior.
                </p>
              </div>
              <div className="mt-2">
                <h2 className="text-primary" style={{ fontSize: "24px" }}>
                  Diverse Talent Base
                </h2>
                <p>
                  Few would argue that, despite the advancements of feminism
                  over the past three decades, women still face a double
                  standard when it comes to their behavior.
                </p>
              </div>
              <div className="mt-2">
                <h2 className="text-primary" style={{ fontSize: "24px" }}>
                  Easy Onboarding
                </h2>
                <p>
                  Few would argue that, despite the advancements of feminism
                  over the past three decades, women still face a double
                  standard when it comes to their behavior.
                </p>
              </div>
            </div>

            <div className="col-lg-7 col-xs-12 mt-5">
              <img
                className=" d-none d-lg-block"
                src={employer}
                alt="facillator"
                style={{
                  position: "absolute",
                  left: "109px",
                }}
              />
              <img
                src={classP}
                alt=""
                className="img-fluid d-lg-none"
                style={{ width: "100%" }}
              />
            </div>
          </div>
        </div>

        <div
          className="session-five d-lg-flex align-items-lg-center space-2 space-top-xl-3 space-bottom-lg-3 space-top-2 space-lg-0 min-vh-lg-100"
          style={{ backgroundColor: "#fff" }}
        >
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
                Let’s help you with hiring
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
              <button className="btn  btn-primary" onClick={handleShow}>
                Start Hiring
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
export default AppForEmployers;
