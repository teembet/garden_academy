import React, { useState } from "react";
import "../assets/css/contactpage.css";
import Socials from "../components/socials";
export interface AppContactUsPageProps {}

const AppContactUsPage: React.SFC<AppContactUsPageProps> = () => {
  const [firstName, setFirstName] = useState("");
  const [firstNameValid, setFirstNameValid] = useState("");
  const [lastName, setLastName] = useState("");
  const [lastNameValid, setLastNameValid] = useState("");
  const [email, setEmail] = useState("");
  const [emailValid, setEmailValid] = useState("");
  const [subject, setSubject] = useState("");
  const [subjectValid, setSubjectValid] = useState("");
  const [message, setMessage] = useState("");
  const [messageValid, setMessageValid] = useState("");

  const handleSubmit = () => {
    if (firstNameValidation(firstName) !== true) {
      return firstNameValidation(firstName);
    }

    if (lastNameValidation(lastName) !== true) {
      return lastNameValidation(lastName);
    }

    if (emailValidation(email) !== true) {
      return emailValidation(email);
    }

    if (subjectValidation(subject) !== true) {
      return subjectValidation(subject);
    }

    if (messageValidation(message) !== true) {
      return messageValidation(message);
    }

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

  const subjectValidation = (fieldValue: string): boolean => {
    if (fieldValue.trim() === "") {
      setSubjectValid(`Subject is required`);
      return false;
    }

    if (fieldValue.trim().length < 10) {
      setSubjectValid(`Subject needs to be at least ten characters`);
      return false;
    }

    setSubjectValid("");
    return true;
  };

  const messageValidation = (fieldValue: string): boolean => {
    if (fieldValue.trim() === "") {
      setMessageValid(`Message is required`);
      return false;
    }

    if (fieldValue.trim().length < 50) {
      setMessageValid(`Message needs to be at least fifty characters`);
      return false;
    }

    setMessageValid("");
    return true;
  };

  return (
    <main id="content" role="main">
      <div className="hero-page">
        <h1 className="d-none d-sm-block">Contact Us</h1>
        <h4 className="d-block d-sm-none">Contact Us</h4>
        <p>&nbsp;</p>
        <p>&nbsp;</p>
      </div>
      <br className="d-block d-lg-none" />
      <br className="d-block d-lg-none" />
      <br className="d-block d-lg-none" />
      <br className="d-block d-lg-none" />
      <br className="d-block d-lg-none" />
      <br className="d-block d-lg-none" />
      <br className="d-block d-lg-none" />
      <br className="d-block d-lg-none" />
      <br className="d-block d-lg-none" />

      <div className="container space-top-6 space-top-lg-4 space-bottom-2">
        <div
          className="card"
          style={{
            height: "inherit",
            padding: "0 5%",
            boxShadow: "0px 4px 10px rgba(143, 148, 155, 0.25)",
            borderRadius: "16px",
            background: "#fff",
            border: "none",
          }}
        >
          <div className="row ">
            <div className="space-right-3 space-top-3 space-bottom-2 col-md-6">
              <div className="mb-4">
                <h2 className="text-primary">
                  You can reach us via any of these mediums.
                </h2>
              </div>

              <ul className="nav nav-sm nav-x-0 nav-white flex-column">
                <h5>Telephone</h5>
                <li className="nav-item">
                  <a className="nav-link media" href="tel:+2348036670001">
                    <span className="media">
                      <span className="fas fa-phone-alt mt-1 mr-2"></span>
                      <span className="media-bodys">
                        +234 [0] 803 667 0001 &nbsp;&nbsp;
                        <br /> +234 [0] 803 667 0001
                      </span>
                    </span>
                  </a>
                </li>
                <br />

                <h5>Email Address</h5>
                <li className="nav-item">
                  <a
                    className="nav-link media"
                    href="mailto:contactus@gardenacademy.com"
                  >
                    <span className="media">
                      <span className="fas fa-phone-alt mt-1 mr-2"></span>
                      <span className="media-bodys">
                        contactus@gardenacademy.com
                      </span>
                    </span>
                  </a>
                </li>
                <br />
                <h5>Office Address</h5>
                <li className="nav-item">
                  <a className="nav-link media" href="/contact">
                    <span className="media">
                      <span className="fas fa-location-arrow mt-1 mr-2"></span>
                      <span className="media-bodys">
                        Vibranium Vally 42, Local Airport Road, Ikeja Lagos
                      </span>
                    </span>
                  </a>
                </li>
              </ul>
              <br />
              <Socials></Socials>
            </div>

            <div className="space-top-2 space-bottom-2 col-md-6">
              <form className="js-validate">
                <div className=" p-4 p-md-6">
                  <div className="row">
                    <div className="col-sm-6">
                      <div className="js-form-message form-group">
                        <label htmlFor="firstName" className="input-label">
                          First name <span className="text-danger">*</span>
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
                          Last name <span className="text-danger">*</span>
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

                    <div className="col-sm-12">
                      <div className="js-form-message form-group">
                        <label htmlFor={"emailAddress"} className="input-label">
                          Email address <span className="text-danger">*</span>
                        </label>
                        <input
                          type="email"
                          className="form-control"
                          name="emailAddress"
                          id="emailAddress"
                          placeholder="eg. Kingsleyomin@gmail.com"
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
                        <label htmlFor={"emailAddress"} className="input-label">
                          Subject <span className="text-danger">*</span>
                        </label>
                        <input
                          type="text"
                          className="form-control"
                          name="emailAddress"
                          id="emailAddress"
                          placeholder="eg. Facilitator enquiry"
                          required
                          value={subject}
                          onChange={(e) => setSubject(e.target.value)}
                          onBlur={(e) => subjectValidation(e.target.value)}
                        />
                        <p className="text-danger">{subjectValid}</p>
                      </div>
                    </div>

                    <div className="col-sm-12">
                      <div className="js-form-message form-group">
                        <label htmlFor={"message"} className="input-label">
                          How can we help you
                          <span className="text-danger">*</span>
                        </label>
                        <div className="input-group">
                          <textarea
                            className="form-control"
                            rows={4}
                            name="message"
                            id="message"
                            placeholder="Enter message here"
                            required
                            value={message}
                            onChange={(e) => setMessage(e.target.value)}
                            onBlur={(e) => messageValidation(e.target.value)}
                          ></textarea>
                        </div>
                        <p>This field is limited to 300 characters</p>
                        <p className="text-danger">{messageValid}</p>
                      </div>
                    </div>
                  </div>

                  <button
                    type="button"
                    style={{ background: "#0F42A4", borderRadius: "2px" }}
                    className="btn btn-block btn-primary transition-3d-hover"
                    onClick={handleSubmit}
                  >
                    Submit
                  </button>
                </div>
              </form>
            </div>
          </div>
        </div>
      </div>
    </main>
  );
};

export default AppContactUsPage;
