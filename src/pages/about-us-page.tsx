import React from "react";
import { Modal, Button } from "react-bootstrap";
import { useState } from "react";

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
// @ts-ignore
import Zoom from "react-reveal/Zoom";
export interface AppAboutUsPageProps {}

const AppAboutUsPage: React.SFC<AppAboutUsPageProps> = () => {
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
                    onChange={(e) => {
                      setFirstName(e.target.value);
                      firstNameValidation(e.target.value);
                    }}
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
                    onChange={(e) => {
                      setLastName(e.target.value);
                      lastNameValidation(e.target.value);
                    }}
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
                    onChange={(e) => {
                      setPhone(e.target.value);
                      phoneValidation(e.target.value);
                    }}
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
                    onChange={(e) => {
                      setEmail(e.target.value);
                      emailValidation(e.target.value);
                    }}
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
                    onChange={(e) => {
                      setUrl(e.target.value);
                      urlValidation(e.target.value);
                    }}
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

export default AppAboutUsPage;
