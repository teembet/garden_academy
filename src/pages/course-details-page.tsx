import { useState, useEffect } from "react";
import { Modal, Button, Alert } from "react-bootstrap";
import { PaystackButton } from "react-paystack";
import { Link } from "react-router-dom";

import "../assets/css/course-detailspage.css";
import PaymentOptions from "../components/payment-options";
import Rating from "../components/rating";
import CourseCardGridView from "../components/course-card-grid-view";

export interface AppCourseDetailsProps {
  config: {};
  showModal: boolean;
  showSecondModal: boolean;
  handleClose: any;
  handleShow: any;
  handleCallBothFuntions: any;
  handleSecondShow: any;
  handleSecondClose: any;
}

const AppCourseDetails: React.SFC<AppCourseDetailsProps> = (props: any) => {
  const [course, setCourse] = useState(props.location?.state?.data);
  const [modules, setModules] = useState(
    course?.modules ? course?.modules : []
  );
  const [amount, setAmount] = useState(
    !course?.price
      ? "10000"
      : course.price === "0.00"
      ? "10000"
      : course.price.replace(".", "")
  );

  const [showModal, setShowModal] = useState(false);
  const [showSecondModal, setShowSecondModal] = useState(false);
  const [firstName, setFirstName] = useState("");
  const [firstNameValid, setFirstNameValid] = useState("");
  const [lastName, setLastName] = useState("");
  const [lastNameValid, setLastNameValid] = useState("");
  const [phone, setPhone] = useState("");
  const [phoneValid, setPhoneValid] = useState("");
  const [email, setEmail] = useState("");
  const [emailValid, setEmailValid] = useState("");
  const [paymentPlan, setPaymentPlan] = useState("one_off");
  const [programs, setPrograms] = useState([]);

  const [showAlert, setShowAlert] = useState({
    text: "",
    show: false,
    color: "success",
  });

  const [buttonText, setButtonText] = useState({
    text: "Next",
    disabled: false,
  });

  const handleClose = () => setShowModal(false);

  const handleShow = () => setShowModal(true);

  const handleSecondClose = () => {
    setShowSecondModal(false);
    handleShow();
  };

  const handleCloseSecondModal = () => {
    setShowSecondModal(false);
  };

  const handleSecondShow = () => setShowSecondModal(true);

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
        setPrograms(loginData.data);
        let result = loginData.data.filter((data: { id: any }) => {
          return data.id == props.match.params.id;
        });

        setCourse(result[0]);
        setModules(course?.modules ? course?.modules : []);
        setAmount(course?.price);
      }
    };

    fetchPrograms();
  }, []);

  const handleCallBothFunctions = async () => {
    if (firstNameValidation(firstName) !== true) {
      return firstNameValidation(firstName);
    }

    if (lastNameValidation(lastName) !== true) {
      return lastNameValidation(lastName);
    }

    if (emailValidation(email) !== true) {
      return emailValidation(email);
    }

    if (phoneValidation(phone) !== true) {
      return phoneValidation(phone);
    }

    setButtonText({
      text: "Loading ...",
      disabled: true,
    });
    try {
      const response = await fetch(
        "https://educollect-api.edutechng.com/api/Edulearn/student",
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
            firstName,
            lastName,
            email,
            phoneNumber: phone,
          }), // body data type must match "Content-Type" header
        }
      );

      let facillatorData = await response.json();

      setButtonText({
        text: "Next",
        disabled: false,
      });

      if (facillatorData.requestSuccessful) {
        setShowAlert({
          text: "Data was submitted successfully",
          show: true,
          color: "success",
        });
        handleClose();
        handleSecondShow();

        setTimeout(() => {
          setShowAlert({
            text: "",
            show: false,
            color: "primary",
          });
          handleClose();
        }, 5000);
      } else {
        setShowAlert({
          text: facillatorData.message,
          show: true,
          color: "danger",
        });
      }
    } catch (error) {
      setButtonText({
        text: "Next",
        disabled: false,
      });
      setShowAlert({
        text: "An Error has Occurred",
        show: true,
        color: "danger",
      });
    }
  };

  const setMoney = (event: any) => {
    setPaymentPlan(event.target.value);
  };

  const config = {
    reference: "garden_academy" + new Date().getTime(),
    email,
    amount: parseInt(amount),
    publicKey: "pk_test_439452ab32de9472427341c35ccc7ef16d32e09c",
    metadata: {
      custom_field: [
        {
          firstName,
          lastName,
          phone,
          email,
        },
      ],
    },
  };

  const handlePaystackSuccessAction = (reference: any) => {
    // Implementation for whatever you want to do with reference and after success call.
    setShowAlert({
      text:
        "Payment was successful we will contact you with the details you submitted",
      show: true,
      color: "success",
    });

    setTimeout(() => {
      setShowAlert({
        text: "",
        show: false,
        color: "primary",
      });
      handleCloseSecondModal();
    }, 10000);
  };

  const handlePaystackCloseAction = () => {
    // implementation for  whatever you want to do when the Paystack dialog closed.
    setShowAlert({
      text:
        "Paystack was closed please make payment to enable us process your course",
      show: true,
      color: "danger",
    });

    setTimeout(() => {
      setShowAlert({
        text: "",
        show: false,
        color: "primary",
      });
    }, 10000);
  };

  const componentProps = {
    ...config,
    text: "Pay Now",
    onSuccess: (reference: any) => handlePaystackSuccessAction(reference),
    onClose: handlePaystackCloseAction,
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

  return (
    <>
      <main id="content" role="main">
        {course ? (
          <div>
            <div
              className="hero-page"
              style={{ alignItems: "baseline", padding: "0 10%" }}
            >
              <div className="row" style={{ width: "100%" }}>
                <div className="col-lg-8">
                  <h1 className="d-none d-sm-block">{course.name}</h1>

                  <h4 className="d-block d-sm-none">{course.name}</h4>

                  <p className="stars">
                    {/* {course.star_count ? course.star_count : "1.00"} */}
                    <Rating rating={course.star_count}></Rating>
                  </p>
                  <p>
                    <i className="far fa-clock"></i>&nbsp;{course.duration}
                    &nbsp;&nbsp;&nbsp; &nbsp;
                    <span>{course.members_count} Students</span>
                  </p>
                </div>
              </div>
            </div>
            <br />
            <div className="container space-top-3 space-top-lg-3 space-bottom-2">
              <div>
                <div className="row ">
                  <div className="space-right-3 space-top-4 space-bottom-2 col-lg-8">
                    <div className="pad space-top-3">
                      <div className="card details" style={{ height: "auto" }}>
                        <div className="head col-lg-12">
                          <h2 className="details-head">
                            About the course you will learn
                          </h2>
                          {/* <p>
                            Students should have basic computer skills and be
                            comfortable navigating online.
                          </p> */}
                        </div>
                        <div className="bullet col-lg-12">
                          <p
                            dangerouslySetInnerHTML={{
                              __html: course.description,
                            }}
                          ></p>
                        </div>
                      </div>
                    </div>
                  </div>

                  <div className=" space-bottom-2 col-lg-4">
                    <div
                      className="card"
                      style={{
                        padding: "0",
                        borderRadius: "4%",
                        border: "1px solid #D7DCE0",
                        boxSizing: "border-box",
                        height: "inherit",
                      }}
                    >
                      <img
                        className="img-fluid card-img-top"
                        src={course.avatar}
                        alt="product design"
                        style={{
                          width: "100%",
                          maxHeight: "20rem",
                          minHeight: "20rem",
                          objectFit: "cover",
                        }}
                      />
                      <div className="card-body">
                        <br />
                        <h2>₦ {course.price}</h2>
                        <br />
                        <button
                          type="submit"
                          style={{
                            background: "#1E944D",
                            border: "2px solid #1E944D",
                          }}
                          onClick={handleShow}
                          className="btn btn-block btn-primary transition-3d-hover"
                          data-toggle="modal"
                          data-target="#exampleModal"
                        >
                          Reserve Your Spot
                        </button>
                        <br />
                        <h5>This course includes:</h5>

                        <ul>
                          {modules.map((data: any, index: number) => {
                            return (
                              <li key={index}>
                                <h5>{data.name}</h5>
                                <p>{data.description}</p>
                              </li>
                            );
                          })}
                        </ul>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        ) : (
          <div
            className="session-four container space-4 space-top-xl-2 space-bottom-lg-2"
            style={{
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
              flexDirection: "column",
            }}
          >
            <div className="space-4">
              <i
                style={{ fontSize: "150px" }}
                className="fas fa-spinner fa-spin "
              ></i>

              <br />
              <br />

              <h1>Loading ...</h1>
            </div>
          </div>
        )}
        <PaymentOptions>
          <div
            className="space-bottom-2"
            style={{
              width: "100%",
              display: "flex",
              justifyContent: "center",
              alignItems: "center",
            }}
          >
            <button
              type="submit"
              style={{
                border: "2px solid #0F42A4",
              }}
              onClick={handleShow}
              className="btn btn-outline-primary transition-3d-hover"
              data-toggle="modal"
              data-target="#exampleModal"
            >
              Reserve Your Spot
            </button>
          </div>
        </PaymentOptions>

        {programs?.length > 0 && (
          <div className="session-four container space-2 space-top-xl-3 space-bottom-lg-3">
            <div className="w-md-80 text-center mx-md-auto mb-5 mb-md-9">
              <h2>Recommended Courses</h2>
              <p>
                Choose from any of our wide range of courses specifically
                tailored to suit your needs.
                <br />
                We have got you covered
              </p>
            </div>
            <section>
              <div className="row mx-n2 mx-lg-n3">
                <CourseCardGridView
                  grid={3}
                  programs={programs.slice(0, 8)}
                ></CourseCardGridView>
              </div>
            </section>
          </div>
        )}
      </main>

      {course && (
        <>
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
                  <div className="offset-2 col-sm-8">
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
                </div>
              </form>
            </Modal.Body>
            <Modal.Footer>
              <Button variant="secondary" onClick={handleClose}>
                Close
              </Button>
              <Button
                disabled={buttonText.disabled}
                variant="primary"
                onClick={handleCallBothFunctions}
              >
                {buttonText.text}
              </Button>
            </Modal.Footer>
          </Modal>

          <Modal
            size="lg"
            aria-labelledby="contained-modal-title-vcenter"
            centered
            show={showSecondModal}
            onHide={handleSecondClose}
          >
            <Modal.Header closeButton>
              <Modal.Title>Reserve a spot</Modal.Title>
            </Modal.Header>
            <Modal.Body>
              <form>
                <div className="container">
                  <div
                    className="row"
                    style={{
                      display: "flex",
                      justifyContent: "center",
                    }}
                    onChange={(e) => setMoney(e)}
                  >
                    <div className=" col-sm-12">
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
                    <div
                      className="col-sm-5 card"
                      style={{
                        height: "inherit",
                        margin: "20px",
                        padding: "20px 0",
                        background: paymentPlan === "one_off" ? "#E8EFFD" : "",
                      }}
                      onClick={() => setPaymentPlan("one_off")}
                    >
                      <div className="row ">
                        <div
                          className="col-3"
                          style={{
                            display: "flex",
                            justifyContent: "center",
                          }}
                        >
                          <input
                            type="radio"
                            style={{ width: "30px", height: "30px" }}
                            id="payment"
                            name="payment"
                            value="one_off"
                            onChange={() => {}}
                            checked={paymentPlan === "one_off"}
                          ></input>
                        </div>

                        <div className="col-9">
                          <h5>ONE OFF PAYMENT</h5>
                          <h2>₦ {course.price}</h2>
                        </div>
                      </div>
                    </div>

                    <div
                      className="col-sm-5 card"
                      style={{
                        height: "inherit",
                        margin: "20px",
                        padding: "20px 0",
                        background:
                          paymentPlan === "instalment" ? "#E8EFFD" : "",
                      }}
                      onClick={() => setPaymentPlan("instalment")}
                    >
                      <div className="row ">
                        <div
                          className="col-3"
                          style={{
                            display: "flex",
                            justifyContent: "center",
                          }}
                        >
                          <input
                            type="radio"
                            style={{ width: "30px", height: "30px" }}
                            aria-label="Radio button for following text input"
                            id="payment"
                            name="payment"
                            value="instalment"
                            onChange={() => {}}
                            checked={paymentPlan === "instalment"}
                          ></input>
                        </div>

                        <div className="col-9">
                          <h5>INSTALLMENT PAYMENT</h5>
                          <h2>Not Available</h2>
                        </div>
                      </div>
                    </div>

                    <div
                      className="col-sm-5 card"
                      style={{
                        height: "inherit",
                        margin: "20px",
                        padding: "20px 0",
                        background: paymentPlan === "loan" ? "#E8EFFD" : "",
                      }}
                      onClick={() => setPaymentPlan("loan")}
                    >
                      <div className="row ">
                        <div
                          className="col-3"
                          style={{
                            display: "flex",
                            justifyContent: "center",
                          }}
                        >
                          <input
                            type="radio"
                            style={{ width: "30px", height: "30px" }}
                            aria-label="Radio button for following text input"
                            id="payment"
                            name="payment"
                            value="loan"
                            onChange={() => {}}
                            checked={paymentPlan === "loan"}
                          ></input>
                        </div>

                        <div className="col-9">
                          <h5>LOAN OFFER</h5>
                          <h2>₦ {course.price}</h2>
                        </div>
                      </div>
                    </div>

                    <div
                      className="col-sm-5 card"
                      style={{
                        height: "inherit",
                        margin: "20px",
                        padding: "20px 0",
                        background: paymentPlan === "deferred" ? "#E8EFFD" : "",
                      }}
                      onClick={() => setPaymentPlan("deferred")}
                    >
                      <div className="row ">
                        <div
                          className="col-3"
                          style={{
                            display: "flex",
                            justifyContent: "center",
                          }}
                        >
                          <input
                            type="radio"
                            style={{ width: "30px", height: "30px" }}
                            aria-label="Radio button for following text input"
                            id="payment"
                            name="payment"
                            value="deferred"
                            onChange={() => {}}
                            checked={paymentPlan === "deferred"}
                          ></input>
                        </div>

                        <div className="col-9">
                          <h5>DEFERRED INCOME SHARE</h5>
                          <h2>₦ {course.price}</h2>
                        </div>
                      </div>
                    </div>
                  </div>
                </div>
              </form>
            </Modal.Body>
            <Modal.Footer>
              <Button variant="secondary" onClick={handleSecondClose}>
                Back
              </Button>
              {paymentPlan === "one_off" ? (
                <PaystackButton
                  className="btn btn-primary"
                  {...componentProps}
                />
              ) : paymentPlan === "instalment" ? (
                <button disabled className="btn btn-primary">
                  Not Available
                </button>
              ) : (
                <a
                  className="btn btn-primary"
                  href="https://ventures_capital.com"
                >
                  Go to Site
                </a>
              )}
            </Modal.Footer>
          </Modal>
        </>
      )}
    </>
  );
};

export default AppCourseDetails;
