import React, { useState } from "react";
import { Alert } from "react-bootstrap";
// @ts-ignore
import Zoom from "react-reveal/Zoom";

import "../assets/css/contactpage.css";
import Socials from "../components/socials";
import { postMethods } from "../helpers/api";

export interface AppContactUsPageProps {}

const AppContactUsPage: React.SFC<AppContactUsPageProps> = () => {
  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    subject: "",
    email: "",
    message: "",
    firstNameValid: "",
    lastNameValid: "",
    subjectValid: "",
    emailValid: "",
    messageValid: "",
  });

  const [showAlert, setShowAlert] = useState({
    text: "",
    show: false,
    color: "success",
  });

  const [buttonText, setButtonText] = useState({
    text: "Submit",
    disabled: false,
  });

  const handleSubmit = async () => {
    if (firstNameValidation(form.firstName) !== true) {
      return firstNameValidation(form.firstName);
    }

    if (lastNameValidation(form.lastName) !== true) {
      return lastNameValidation(form.lastName);
    }

    if (emailValidation(form.email) !== true) {
      return emailValidation(form.email);
    }

    if (subjectValidation(form.subject) !== true) {
      return subjectValidation(form.subject);
    }

    if (messageValidation(form.message) !== true) {
      return messageValidation(form.message);
    }

    setButtonText({
      text: "Loading ...",
      disabled: true,
    });

    const res = await postMethods("/MailingList/sendmail", {
      recipientEmail: "info@gardenacademy.io",
      name: "Edutech Support Page",
      message: `First Name :   ${form.firstName}
                    <br/>
                    Last Name:   ${form.lastName}
                    <br/>
                    Email:   ${form.email}
                    <br/>
                    Subject:   ${form.subject}
                    <br/>
                    Message: ${form.message}`,
    });

    if (res.status) {
      setButtonText({
        text: "Submit",
        disabled: false,
      });

      setShowAlert({
        text: "Contact us information was submitted successfully.",
        show: true,
        color: "success",
      });

      setForm({
        firstName: "",
        lastName: "",
        subject: "",
        email: "",
        message: "",
        firstNameValid: "",
        lastNameValid: "",
        subjectValid: "",
        emailValid: "",
        messageValid: "",
      });
    } else {
      setShowAlert({
        text: "Contact us information was not successful.",
        show: true,
        color: "danger",
      });
    }
  };

  const firstNameValidation = (fieldValue: string): boolean => {
    if (fieldValue.trim() === "") {
      setForm({ ...form, firstNameValid: "First name is required" });

      return false;
    }

    if (/[^a-zA-Z -]/.test(fieldValue)) {
      setForm({ ...form, firstNameValid: "Invalid characters" });

      return false;
    }

    if (fieldValue.trim().length < 3) {
      setForm({
        ...form,
        firstNameValid: "First name needs to be at least 3 characters",
      });

      return false;
    }
    setForm({ ...form, firstNameValid: "" });
    return true;
  };

  const lastNameValidation = (fieldValue: string): boolean => {
    if (fieldValue.trim() === "") {
      setForm({ ...form, lastNameValid: "Last name is required" });
      return false;
    }

    if (/[^a-zA-Z -]/.test(fieldValue)) {
      setForm({ ...form, lastNameValid: "Invalid characters" });
      return false;
    }

    if (fieldValue.trim().length < 3) {
      setForm({
        ...form,
        lastNameValid: "Last name needs to be at least 3 characters",
      });
      return false;
    }
    setForm({ ...form, lastNameValid: "" });
    return true;
  };

  const emailValidation = (email: string): boolean => {
    if (
      /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/.test(
        email
      )
    ) {
      setForm({ ...form, emailValid: "" });
      return true;
    }
    if (email.trim() === "") {
      setForm({ ...form, emailValid: "Email is required" });
      return false;
    }
    setForm({ ...form, emailValid: "Please enter a valid email" });
    return false;
  };

  const subjectValidation = (fieldValue: string): boolean => {
    if (fieldValue.trim() === "") {
      setForm({ ...form, subjectValid: "Subject is required" });
      return false;
    }

    if (fieldValue.trim().length < 10) {
      setForm({
        ...form,
        subjectValid: "Subject needs to be at least 10 characters",
      });
      return false;
    }

    setForm({ ...form, subjectValid: "" });
    return true;
  };

  const messageValidation = (fieldValue: string): boolean => {
    if (fieldValue.trim() === "") {
      setForm({ ...form, messageValid: "Message is required" });
      return false;
    }

    if (fieldValue.trim().length < 50) {
      setForm({
        ...form,
        messageValid: "Message needs to be at least 50 characters",
      });
      return false;
    }

    if (fieldValue.trim().length > 300) {
      setForm({
        ...form,
        messageValid: "Message is limited to 300 words, keep it short",
      });
      return false;
    }
    setForm({ ...form, messageValid: "" });
    return true;
  };

  return (
    <main id="content" role="main">
      <div className="hero-page">
        <h1 className="d-none d-sm-block animated slideInDown">Contact Us</h1>
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
      <br />
      <br />
      <Zoom>
        <div className="container space-top-6 space-top-lg-4 space-bottom-2">
          <div
            className="card card-hover"
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
                          info@gardenacademy.io
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
                          Vibranium Valley 42, Local Airport Road, Ikeja Lagos
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
                      <div className="col-sm-12">
                        <Alert
                          show={showAlert.show}
                          variant={showAlert.color}
                          onClose={() =>
                            setShowAlert({ ...showAlert, show: false })
                          }
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
                            First name <span className="text-danger">*</span>
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
                            Last name <span className="text-danger">*</span>
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

                      <div className="col-sm-12">
                        <div className="js-form-message form-group">
                          <label
                            htmlFor={"emailAddress"}
                            className="input-label"
                          >
                            Email address <span className="text-danger">*</span>
                          </label>
                          <input
                            type="email"
                            className="form-control"
                            name="emailAddress"
                            id="emailAddress"
                            placeholder="eg. Kingsleyomin@gmail.com"
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
                          <label
                            htmlFor={"emailAddress"}
                            className="input-label"
                          >
                            Subject <span className="text-danger">*</span>
                          </label>
                          <input
                            type="text"
                            className="form-control"
                            name="emailAddress"
                            id="emailAddress"
                            placeholder="eg. Facilitator enquiry"
                            required
                            value={form.subject}
                            onChange={(e) => {
                              setForm({ ...form, subject: e.target.value });
                            }}
                            onBlur={(e) => {
                              subjectValidation(e.target.value);
                            }}
                          />
                          <p className="text-danger">{form.subjectValid}</p>
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
                              value={form.message}
                              onChange={(e) => {
                                setForm({ ...form, message: e.target.value });
                              }}
                              onBlur={(e) => {
                                messageValidation(e.target.value);
                              }}
                            ></textarea>
                          </div>
                          <p>This field is limited to 300 characters</p>
                          <p className="text-danger">{form.messageValid}</p>
                        </div>
                      </div>
                    </div>

                    <button
                      type="button"
                      disabled={buttonText.disabled}
                      style={{ background: "#0F42A4", borderRadius: "2px" }}
                      className="btn btn-block btn-primary transition-3d-hover"
                      onClick={handleSubmit}
                    >
                      {buttonText.text}
                    </button>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      </Zoom>
      <div style={{ display: "none" }}>
        * Edutech Project <br />
        * Done By Satowind (Ogugua Tochukwu) <br />
        * 08038385263, Evensatowind@gmail.com <br />
        *Satoseries (Web app and mobile App engineering) <br />* For Ventures
        Garden Group
      </div>
    </main>
  );
};

export default AppContactUsPage;
