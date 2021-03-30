import React from "react";
import { Modal, Button, Alert } from "react-bootstrap";
import { useState } from "react";
// @ts-ignore
import Zoom from "react-reveal/Zoom";

import "../assets/css/aboutpage.css";
import girl from "../assets/img/girl.png";
import Arlene from "../assets/img/Arlene.png";
import a from "../assets/img/a.png";
import b from "../assets/img/b.png";
import c from "../assets/img/c.png";
import d from "../assets/img/d.png";
import e from "../assets/img/e.png";
import f from "../assets/img/f.png";
import g from "../assets/img/g.png";
import h from "../assets/img/h.png";
import i from "../assets/img/i.png";
import j from "../assets/img/j.png";
import k from "../assets/img/k.png";
import study from "../assets/img/study.png";
import { postMethods } from "../helpers/api";
export interface AppAboutUsPageProps {}

const AppAboutUsPage: React.SFC<AppAboutUsPageProps> = () => {
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

  const [showAlert, setShowAlert] = useState({
    text: "",
    show: false,
    color: "success",
  });

  const [buttonText, setButtonText] = useState({
    text: "Submit",
    disabled: false,
  });

  const [showModal, setShowModal] = useState(false);
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
        <div className="hero-page-about">
          <h1 className="d-none d-sm-block  animated slideInDown">
            The Garden Academy Team
          </h1>

          <p className="d-none d-sm-block  animated slideInUp">
            Choose from any of our wide range of courses tailored to suit your
            needs.
            <br /> From software engineering to product management and more, We
            have got you covered
          </p>

          <h4 className="d-block d-sm-none  animated slideInDown">
            The Garden Academy Team
          </h4>

          <p
            style={{ fontSize: ".9em" }}
            className="d-block d-sm-none  animated slideInUp"
          >
            Choose from any of our wide range of courses tailored to suit your
            needs. From software engineering to product management and more, We
            have got you covered
          </p>
        </div>
        <br />
        <div className="container space-bottom-2">
          <div className="row space-top-3 space-bottom-2">
            <div className=" col-md-6">
              <div className="mb-4">
                <div className="top"></div>
                <h1>About Us</h1>
              </div>

              <br />

              <div className="row" style={{ margin: "0 5px" }}>
                <p>
                  Garden Academy is a subsidiary of EduTech, a player in the
                  global edtech industry which has sought to break down barriers
                  to education across all levels worldwide, with a special focus
                  on the African continent.
                </p>

                <p>
                  Garden Academy was borne of the desire of progressive
                  individuals to get more Africans into tech in different
                  functions to get these professionals on a level playing field
                  with their global counterparts.
                </p>
                <p>
                  We are passionate about grooming the next crop of tech
                  superstars who can further drive development across all
                  sectors by leveraging technology.
                </p>
              </div>
            </div>

            <div
              className="col-md-6"
              style={{ padding: "5%", position: "relative" }}
            >
              <img
                src={girl}
                alt=""
                className="img-fluid img-data d-none d-lg-block card-hover"
              />
              <img
                src={study}
                alt=""
                className="img-fluid img-data d-lg-none"
              />

              <div className="box d-none d-lg-block"></div>
            </div>
          </div>
          <Zoom>
            <div className="container space-bottom-2">
              <div className="d-none d-lg-block d-md-block">
                <h1 style={{ textAlign: "center" }}>Meet Our Facilitators</h1>
                <p style={{ textAlign: "center" }}>
                  We have carefully chosen a team of experienced and hardworking
                  people in different fields <br />
                  to train the next set of tech professionals.
                </p>
              </div>
              <div className="d-block d-lg-none d-md-none">
                <h1>Meet Our Facilitators</h1>
                <p>
                  We have carefully chosen a team of experienced and hardworking
                  people in different fields to train the next set of tech
                  professionals.
                </p>
              </div>
              <br />
              <br />
              <div className="row">
                <div className="col-md-3 card-hover">
                  <img src={Arlene} alt="" className="img-fluid img-data" />
                  <br />
                  <br />
                  <h5>Arlene McCoy</h5>
                  <p>Software Engineering Facilitator</p>
                  <br />
                </div>
                <div className="col-md-3 card-hover">
                  <img src={a} alt="" className="img-fluid img-data" />
                  <br />
                  <br />
                  <h5>Arlene McCoy</h5>
                  <p>Software Engineering Facilitator</p>
                  <br />
                </div>
                <div className="col-md-3 card-hover">
                  <img src={b} alt="" className="img-fluid img-data" />
                  <br />
                  <br />
                  <h5>Arlene McCoy</h5>
                  <p>Software Engineering Facilitator</p>
                  <br />
                </div>
                <div className="col-md-3 card-hover">
                  <img src={c} alt="" className="img-fluid img-data" />
                  <br />
                  <br />
                  <h5>Arlene McCoy</h5>
                  <p>Software Engineering Facilitator</p>
                  <br />
                </div>
                <div className="col-md-3 card-hover">
                  <img src={d} alt="" className="img-fluid img-data" />
                  <br />
                  <br />
                  <h5>Arlene McCoy</h5>
                  <p>Software Engineering Facilitator</p>
                  <br />
                </div>
                <div className="col-md-3 card-hover">
                  <img src={e} alt="" className="img-fluid img-data" />
                  <br />
                  <br />
                  <h5>Arlene McCoy</h5>
                  <p>Software Engineering Facilitator</p>
                  <br />
                </div>
                <div className="col-md-3 card-hover">
                  <img src={f} alt="" className="img-fluid img-data" />
                  <br />
                  <br />
                  <h5>Arlene McCoy</h5>
                  <p>Software Engineering Facilitator</p>
                  <br />
                </div>
                <div className="col-md-3 card-hover">
                  <img src={g} alt="" className="img-fluid img-data" />
                  <br />
                  <br />
                  <h5>Arlene McCoy</h5>
                  <p>Software Engineering Facilitator</p>
                  <br />
                </div>
                <div className="col-md-3 card-hover">
                  <img src={h} alt="" className="img-fluid img-data" />
                  <br />
                  <br />
                  <h5>Arlene McCoy</h5>
                  <p>Software Engineering Facilitator</p>
                  <br />
                </div>
                <div className="col-md-3 card-hover">
                  <img src={i} alt="" className="img-fluid img-data" />
                  <br />
                  <br />
                  <h5>Arlene McCoy</h5>
                  <p>Software Engineering Facilitator</p>
                  <br />
                </div>
                <div className="col-md-3 card-hover">
                  <img src={j} alt="" className="img-fluid img-data" />
                  <br />
                  <br />
                  <h5>Arlene McCoy</h5>
                  <p>Software Engineering Facilitator</p>
                  <br />
                </div>
                <div className="col-md-3 card-hover">
                  <img src={k} alt="" className="img-fluid img-data" />
                  <br />
                  <br />
                  <h5>Arlene McCoy</h5>
                  <p>Software Engineering Facilitator</p>
                  <br />
                </div>
              </div>
            </div>
          </Zoom>
          <div className="action-box space-3">
            <h2 style={{ textAlign: "center" }}>
              Do you have what it takes to train the <br />
              next set of tech superstars?
            </h2>
            <br />
            <button
              type="submit"
              className="btn btn-primary transition-3d-hover"
              onClick={handleShow}
            >
              Become a Facilitator
            </button>
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

export default AppAboutUsPage;
