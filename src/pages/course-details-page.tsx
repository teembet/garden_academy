import { useState } from 'react';
import { Modal, Button } from 'react-bootstrap';

import '../assets/css/course-detailspage.css'
import { FaStar } from "react-icons/fa";
import bitmap from "../assets/img/bitmap.png";
import PaymentOptions from '../components/payment-options'

export interface AppCourseDetailsProps {

}

const AppCourseDetails: React.SFC<AppCourseDetailsProps> = () => {

    // const closeModal = () => {
    //     $('#exampleModal').modal('hide')
    // }

    const [showModal, setShowModal] = useState(false);
    const [showSecondModal, setShowSecondModal] = useState(false);

    const handleClose = () => setShowModal(false);
    const handleShow = () => setShowModal(true);

    const handleSecondClose = () => setShowSecondModal(false);
    const handleSecondShow = () => setShowSecondModal(true);

    const handleCallBothFuntions = () => {
        handleClose()
        handleSecondShow()
    }

    const handleAlert = () => {
        handleSecondClose()
        alert("Success")
    }

    return (
        <>
            <main id="content" role="main">
                <div>

                    <div className="hero-page" style={{ alignItems: "baseline", padding: "0 10%" }}>
                        <h1>User Experience Design Fundamentals</h1>
                        <p>
                            Design Web Sites and Mobile Apps that Your Users Love and Return to Again and Again
                    <br />  with UX Expert Kingsley Omin.
                </p>

                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /></p>

                        <span>302,000 students</span>


                    </div>
                    <br />
                    <div className="container space-top-3 space-top-lg-3 space-bottom-2">
                        <div >
                            <div className="row ">

                                <div className="space-right-3 space-top-5 space-bottom-2 col-lg-8">

                                    <div className="pad space-top-3">
                                        <div className="card details" style={{ height: "auto" }}>
                                            <div className="head col-lg-12">
                                                <h2 className="details-head">About the course you will learn</h2>
                                                <p>Students should have basic computer skills and be comfortable navigating online.</p>
                                            </div>
                                            <div className="bullet col-lg-12">
                                                <ul typeof="disc" className="list-group">
                                                    <li>
                                                        <h3>User experience fundamentals</h3>
                                                        <p>
                                                            In this course, we give you a framework to help you organize and plan your marketing approach. We also introduce you to three companies that are featured throughout the Digital Marketing Nanodegree program as examples of how to apply what you learn in both B2C and B2B contexts.
            </p>
                                                    </li>
                                                    <li>
                                                        <h3>Content strategy</h3>
                                                        <p>
                                                            Content is at the core of all marketing activity. In this course you learn how to plan your content marketing, how to develop content that works well for your target audience, and how to measure its impact.
            </p>
                                                    </li>
                                                    <li>
                                                        <h3>User experience fundamentals</h3>
                                                        <p>
                                                            Social media is a powerful channel for marketers. In this course, you learn more about the main social media platforms, how to manage your social media presence, and how to create effective content for each platform.
            </p>
                                                    </li>
                                                    <li>
                                                        <h3>
                                                            Email marketing
            </h3>
                                                        <p>
                                                            Email is an effective marketing channel, especially at the conversion and retention stage of the customer journey. In this course, you learn how to create an email marketing strategy, create and execute email campaigns, and measure the results.
            </p>
                                                    </li>
                                                    <li>
                                                        <h3>
                                                            User experience fundamentals
            </h3>
                                                        <p>
                                                            Social media is a powerful channel for marketers. In this course, you learn more about the main social media platforms, how to manage your social media presence, and how to create effective content for each platform.
            </p>
                                                    </li>
                                                </ul>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div className=" space-bottom-2 col-lg-4">

                                    <div className="card" style={{
                                        padding: "0", borderRadius: "4%", border: "1px solid #D7DCE0", boxSizing: "border-box", height: "inherit"
                                    }}>
                                        <img className="img-fluid card-img-top" src={bitmap} alt="product design" style={{ width: "100%" }} />
                                        <div className="card-body">
                                            <br />
                                            <h2>NGN250,000</h2>
                                            <br />
                                            <button type="submit" style={{ background: "#1E944D", border: "2px solid #1E944D" }} onClick={handleShow} className="btn btn-block btn-primary transition-3d-hover" data-toggle="modal" data-target="#exampleModal">Reserve Your Spot
                                            </button>
                                            <br />
                                            <h5>This course includes:</h5>

                                            <ul>
                                                <li>
                                                    <p>10 hours on-demand video</p>
                                                </li>
                                                <li>
                                                    <p>Full lifetime access</p>
                                                </li>
                                                <li>
                                                    <p>Access on mobile and TV</p>
                                                </li>
                                                <li>
                                                    <p>34 Downloadable materials</p>
                                                </li>
                                                <li>
                                                    <p>Certificate of completion</p>
                                                </li>
                                            </ul>

                                        </div>
                                    </div>

                                </div>
                            </div>


                        </div>

                    </div>
                </div>

                <PaymentOptions></PaymentOptions>
            </main >

            <Modal
                size="lg"
                aria-labelledby="contained-modal-title-vcenter"
                centered show={showModal} onHide={handleClose}>
                <Modal.Header closeButton>
                    <Modal.Title id="contained-modal-title-vcenter">Reserve a spot</Modal.Title>
                </Modal.Header >
                <Modal.Body>
                    <form>
                        <div className="row">
                            <div className="col-sm-6">

                                <div className="js-form-message form-group">
                                    <label htmlFor={"firstName"} className="input-label">First name</label>
                                    <input type="text" className="form-control" name="firstName" id="firstName" placeholder="eg. Nataly" aria-label="Nataly" required data-msg="Please enter first your name" />
                                </div>

                            </div>

                            <div className="col-sm-6">

                                <div className="js-form-message form-group">
                                    <label htmlFor={"lastName"} className="input-label">Last name</label>
                                    <input type="text" className="form-control" name="lastName" id="lastName" placeholder="eg. Gaga" aria-label="Gaga" required data-msg="Please enter last your name" />
                                </div>

                            </div>

                            <div className="col-sm-6">

                                <div className="js-form-message form-group">
                                    <label htmlFor={"firstName"} className="input-label">Phone Number</label>
                                    <input type="tel" className="form-control" name="firstName" id="firstName" placeholder="08045275625" aria-label="Nataly" required data-msg="Please enter first your name" />
                                </div>

                            </div>

                            <div className="col-sm-6">

                                <div className="js-form-message form-group">
                                    <label htmlFor={"lastName"} className="input-label">Email Address</label>
                                    <input type="email" className="form-control" name="lastName" id="lastName" placeholder="admin@gmail.com" aria-label="Gaga" required data-msg="Please enter last your name" />
                                </div>

                            </div>
                        </div>

                    </form>

                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleClose}>
                        Close
          </Button>
                    <Button variant="primary" onClick={handleCallBothFuntions}>
                        Next
          </Button>
                </Modal.Footer>
            </Modal>

            <Modal
                size="lg"
                aria-labelledby="contained-modal-title-vcenter" centered show={showSecondModal} onHide={handleSecondClose}>
                <Modal.Header closeButton>
                    <Modal.Title>Reserve a spot</Modal.Title>
                </Modal.Header>
                <Modal.Body>
                    <form>
                        <div className="container">


                            <div className="row" style={{
                                display: "flex",
                                justifyContent: "center"
                            }}>
                                <div className="col-sm-5 card" style={{ height: "inherit", margin: "20px" }}>

                                    <div className="row ">
                                        <div className="col-sm-3" style={{
                                            display: "flex",
                                            justifyContent: "center"
                                        }}>
                                            <input type="radio" style={{ width: "30px", height: "30px" }} aria-label="Radio button for following text input" id="vehicle1" name="money" value="1"></input>
                                        </div>

                                        <div className="col-sm-9">
                                            <h5>ONE OFF PAYMENT</h5>
                                            <h2>NGN120,000</h2>

                                        </div>
                                    </div>



                                </div>

                                <div className="col-sm-5 card" style={{ height: "inherit", margin: "20px" }}>

                                    <div className="row ">
                                        <div className="col-sm-3" style={{
                                            display: "flex",
                                            justifyContent: "center"
                                        }}>
                                            <input type="radio" style={{ width: "30px", height: "30px" }} aria-label="Radio button for following text input" id="vehicle2" name="money" value="2"></input>
                                        </div>

                                        <div className="col-sm-9">
                                            <h5>INSTALLMENTAL PAYMENT</h5>
                                            <h2>NGN120,000</h2>

                                        </div>
                                    </div>



                                </div>

                                <div className="col-sm-5 card" style={{ height: "inherit", margin: "20px" }}>

                                    <div className="row ">
                                        <div className="col-sm-3" style={{
                                            display: "flex",
                                            justifyContent: "center"
                                        }}>
                                            <input type="radio" style={{ width: "30px", height: "30px" }} aria-label="Radio button for following text input" id="vehicle3" name="money" value="3"></input>
                                        </div>

                                        <div className="col-sm-9">
                                            <h5>LOAN OFFER</h5>
                                            <h2>NGN120,000</h2>

                                        </div>
                                    </div>



                                </div>

                                <div className="col-sm-5 card" style={{ height: "inherit", margin: "20px" }}>

                                    <div className="row ">
                                        <div className="col-sm-3" style={{
                                            display: "flex",
                                            justifyContent: "center"
                                        }}>
                                            <input type="radio" style={{ width: "30px", height: "30px" }} aria-label="Radio button for following text input" id="vehicle4" name="money" value="4"></input>
                                        </div>

                                        <div className="col-sm-9">
                                            <h5>DEFERRED INCOME SHARE</h5>
                                            <h2>NGN120,000</h2>

                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                    </form>

                </Modal.Body>
                <Modal.Footer>
                    <Button variant="secondary" onClick={handleSecondClose}>
                        Close
          </Button>
                    <Button variant="primary" onClick={handleAlert}>
                        Next
          </Button>
                </Modal.Footer>
            </Modal>
        </>
    );
}

export default AppCourseDetails;