
import * as React from 'react';
import { Link } from 'react-router-dom'
import { Modal, Button } from 'react-bootstrap';
import { useState } from 'react';

import '../assets/css/homepage.css'
import rectangle from "../assets/img/rectangle.svg";
import facilitator from "../assets/img/facilitator.svg"
import homehero from "../assets/img/home-hero.svg";
import homehero2 from "../assets/img/home-hero2.svg";
import homecard1 from "../assets/img/homecard1.svg";
import homecard2 from "../assets/img/homecard2.svg";
import homecard3 from "../assets/img/homecard3.svg";
import homecard4 from "../assets/img/homecard4.svg";
import homecard5 from "../assets/img/homecard5.svg";
import homecard6 from "../assets/img/homecard6.svg";
import womanPic from '../assets/img/woman.svg';
import person2 from '../assets/img/person2.svg';
import person1 from '../assets/img/person1.svg';
import pd1 from "../assets/img/pd1.svg";
import pd2 from "../assets/img/pd2.svg";
import pd3 from "../assets/img/pd3.svg";
import CourseCard from '../components/course-card'
import Search from '../components/search'

export interface AppHomePageProps {

}

const AppHomePage: React.SFC<AppHomePageProps> = () => {

    const searchCourse = () => {
        console.log("wind")
    }

    const [showModal, setShowModal] = useState(false);

    const handleClose = () => setShowModal(false);
    const handleShow = () => setShowModal(true);
    const handleSubmit = () => alert("Success")

    return (
        <>

            <main id="content" role="main">
                {/* <!-- Hero Section --> */}
                <div className="d-lg-flex position-relative hero" style={{ paddingBottom: "5%" }}>
                    <div className="container d-lg-flex  align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
                        {/* <!-- Content --> */}
                        <div className="w-md-100">
                            <div className="row">
                                <div className="col-lg-6">
                                    <div className="mb-5 mt-11">
                                        <h1 className="display-4 mb-3">
                                            Learn the <span className="text-primary">skills</span>   you
                                            need to <span className="text-primary">succeed</span>

                                            <span className="text-primary text-highlight-warning">
                                                <span className="js-text-animation"
                                                    data-hs-typed-options='{
                            "strings": ["startup.", "future.", "success."],
                            "typeSpeed": 90,
                            "loop": true,
                            "backSpeed": 30,
                            "backDelay": 2500
                          }'></span>
                                            </span>
                                        </h1>
                                        <p className="lead"> We are committed to training the next generation of tech superstars and help organisations upscale
                        their workforce with the right talent</p>
                                    </div>

                                    <Search search={"What do you want to learn"} button_text={"Search"} onSearchSubmit={searchCourse} ></Search>
                                    <br /><br />
                                </div>

                                <div className="col-lg-6">
                                    <img src={homehero} alt="" className="img-fluid img-fluid d-none d-lg-block" style={{
                                        position: "absolute",
                                        right: 0
                                    }} />
                                    <img src={womanPic} alt="" className="img-fluid d-lg-none" style={{ width: "100%" }} />
                                    {/* <br className="d-lg-none d-md-none" />
                                    <br className="d-lg-none d-md-none" /> */}
                                </div>
                            </div>
                        </div>
                        {/* <!-- End Content -->

       
          {/* <!-- End SVG Shape --> */}
                    </div>
                </div>
                {/* <!-- End Hero Section -->

      <!-- Articles Section --> */}
                <div className="container space-2 space-top-xl-3 space-bottom-lg-3">
                    {/* <!-- Title --> */}
                    <div className="w-md-80 w-lg-50 text-center mx-md-auto mb-5 mb-md-9">
                        <h2>Why Garden Academy</h2>
                        <p>Pellentesque donec ut accumsan nibh turpis massa facilisis pellentesque amet.</p>
                    </div>
                    {/* <!-- End Title --> */}

                    <div className="row mx-n2 mx-lg-n3">
                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3" >
                            {/* <!-- Card --> */}
                            <div className="card">
                                <div className="card-icon">
                                    <span className="span-icon"> <img src={homecard1} alt="" /> </span>
                                </div>
                                <h3 className="card-headers">Top Industry Facilitators</h3>
                                <div className="card-body">Learn from Subject matter experts from different areas of the tech industry and gain the knowledge you need to rise to the top of your field. </div>
                            </div>

                            {/* <!-- End Card --> */}
                        </div>
                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3" >
                            {/* <!-- Card --> */}
                            <div className="card">
                                <div className="card-icon">
                                    <span className="span-icon"> <img src={homecard2} alt="" /> </span>
                                </div>
                                <h3 className="card-headers">Resume & Interview Prep</h3>
                                <div className="card-body">Gain valuable tips and hacks you need to create an appealing resume and navigate interview scenarios. </div>
                            </div>

                            {/* <!-- End Card --> */}
                        </div>

                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3" >
                            {/* <!-- Card --> */}
                            <div className="card">
                                <div className="card-icon">
                                    <span className="span-icon"> <img src={homecard3} alt="" /> </span>
                                </div>
                                <h3 className="card-headers">Flexible Learning</h3>
                                <div className="card-body">Learn wherever, whenever with quality content delivered to your device on demand. Powered by VigiLearnLMS™.</div>
                            </div>

                            {/* <!-- End Card --> */}
                        </div>
                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3" >
                            {/* <!-- Card --> */}
                            <div className="card">
                                <div className="card-icon">
                                    <span className="span-icon"> <img src={homecard4} alt="" /> </span>
                                </div>
                                <h3 className="card-headers">Flexible Payment Options</h3>
                                <div className="card-body">Take advantage of any of our available fee payment options and enjoy unrivalled ease of access. </div>
                            </div>

                            {/* <!-- End Card --> */}
                        </div>
                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3" >
                            {/* <!-- Card --> */}
                            <div className="card">
                                <div className="card-icon">
                                    <span className="span-icon"> <img src={homecard5} alt="" /> </span>
                                </div>
                                <h3 className="card-headers">Globally Recognized <br />  Certificate</h3>
                                <div className="card-body">Receive a certificate of international repute upon completion of your chosen learning path.</div>
                            </div>

                            {/* <!-- End Card --> */}
                        </div>
                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3" >
                            {/* <!-- Card --> */}
                            <div className="card">
                                <div className="card-icon">
                                    <span className="span-icon"> <img src={homecard6} alt="" /> </span>
                                </div>
                                <h3 className="card-headers">Internship & Full-time <br />    opportunities</h3>
                                <div className="card-body">Put your newly acquired skills to use with access to work opportunities across the global tech industry.</div>
                            </div>

                            {/* <!-- End Card --> */}
                        </div>



                    </div >
                </div >
                {/* <!-- End Articles Section --> */}
                <div className=" d-lg-flex position-relative session-three" >
                    <div className=" container d-lg-flex align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100" >
                        {/* <!-- Card --> */}
                        <section className=" ">
                            <div className="img2-container mb-4">
                                <img className="img-fluid img2-style" src={homehero2} alt="" />
                            </div>
                            <div className="container">
                                <h1 className="font-weight-bold white-text">Looking To Improve Your Workforce?</h1>
                                <p style={{ marginTop: "20px", textAlign: "left", fontSize: "18px" }}>The terrain of the global tech industry is rapidly evolving, and it is imperative that your workforce stays empowered and relevant in today’s world. By exposing your personnel to quality learning opportunities on Garden Academy, their horizons are broadened, and they become empowered to compete on the global playing field while contributing their newly acquired skills to the growth of your organization.</p>

                                <div className="row" style={{ marginTop: "70px" }}>
                                    <div className="col-md-4 mb-3" style={{ padding: "0px" }}>
                                        <div className="card-icon">
                                            <span>
                                                <img src={rectangle} alt="" />
                                            </span>
                                        </div>
                                        <h3 className="card-headers white-text">Hire Talent</h3>
                                        <div className="card-body s3-para">
                                            Garden Academy harnesses the knowledge & experience of global industry experts and channels it into truly expansive courses that create the best talent who are primed and ready to take your organization to the next level.                  </div>
                                    </div>
                                    <div className="col-md-4 mb-3">
                                        <div className="card-icon">
                                            <span><img src={rectangle} alt="" /> </span>
                                        </div>
                                        <h3 className="card-headers white-text">Upskill Existing Staff</h3>
                                        <div className="card-body s3-para">
                                            With the best facilitators delivering quality content, Garden Academy vastly improves the skill level of staff groups. Your staff will gain the skills needed to improve business outcomes and contribute directly to your organization’s bottom line                    </div>
                                    </div>
                                    <div className="col-md-4 mb-3">
                                        <div className="card-icon">
                                            <span><img src={rectangle} alt="" /> </span>
                                        </div>
                                        <h3 className="card-headers white-text">Staff Onboarding</h3>
                                        <div className="card-body s3-para">
                                            With the best facilitators delivering quality content, Garden Academy vastly improves the skill level of staff groups. Your staff will gain the skills needed to improve business outcomes and contribute directly to your organization’s bottom line                    </div>
                                    </div>
                                </div>

                                <div className="get-started">
                                    <button className="btn get-started-btn">Get Started</button>
                                </div>

                            </div>
                        </section>
                        {/* <!-- End Card --> */}
                    </div>
                </div >

                <div className="session-four container space-2 space-top-xl-3 space-bottom-lg-3">
                    <div className="w-md-80 text-center mx-md-auto mb-5 mb-md-9">
                        <h2>Available Programs</h2>
                        <p>Select any program from our library of carefully crafted programs guaranted to take you </p>
                    </div>
                    <section>

                        <div className="row mx-n2 mx-lg-n3">
                            <CourseCard images={pd1} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd2} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd3} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>
                        </div>

                    </section>

                    <div className="get-started">
                        <Link to="/programs" className="btn programs-btn"><b>View All Programs</b></Link>
                    </div>

                </div>

                <div className="session-five d-lg-flex align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
                    <div className="row space-bottom-2" style={{ margin: "1% 5%" }}>
                        <div className="col-lg-5 mt-5">
                            <hr className="mt-5" style={{ width: "10%", border: "2px solid #0B2253", opacity: "0.5", margin: "0px" }} />
                            <h3 className="mt-3" style={{ fontSize: "36px", color: "#041644" }}>Become a Facilator</h3>
                            <p className="mt-3 facillator-p">Lorem ipsum dolor sit amet, consectetur vred adipiscing tortor, pellentesque donec deaut accumsan nibh turpis eu massa consectetur adipiscing tortor benelit.
                            </p>
                            <br />
                            <p className="facillator-p">
                                Lorem ipsum dolor sit amet, consectetur vred adipiscing adipiscing tortor, pellentesque donec deaut accumsan nibh turpis pellentesque donec deaut consectetur.
                   </p>
                            <br />
                            <br />
                            <button onClick={handleShow} className="btn btn-lg  btn-primary">Become a Facillator</button>


                        </div>

                        <div className="col-lg-7 col-xs-12 mt-5">
                            <img className="img-fluid d-lg-block" src={facilitator} alt="facillator" />
                        </div>
                    </div>
                </div>

                <div className=" container-fluid d-lg-flex  align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
                    <div className="row " style={{ margin: "1% 5%" }}>
                        <div className="col-lg-4 mt-5">
                            <h4 className="mt-3 testimonials-heading" >Testimonials</h4>
                            <p className="mt-3" style={{ fontSize: "35px" }}>Read what our users have to say...</p>
                            <br /><br /><br /><br />
                            <div style={{ display: "flex", justifyContent: "space-around", alignItems: "center" }}>
                                <span className="fa fa-arrow-left slick-arrow slick-arrow-primary-white slick-arrow-left shadow-soft rounded-circle ml-sm-n2"></span>
                                <h4>1 / 4</h4>
                                <span className="fa fa-arrow-right slick-arrow slick-arrow-primary-white slick-arrow-right shadow-soft rounded-circle mr-sm-2 mr-xl-4"></span>
                            </div>



                        </div>

                        <div className="col-lg-8 mt-5">
                            <div className="row ">
                                <div className=" col-md-6 col-sm-12 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                    <div className="card  shadow pt-3 pb-5 px-2" style={{ height: "auto" }}>
                                        <div className="row card-icon" style={{ marginBottom: "0 !important" }}>
                                            <div className="col-3"><img className="avatar img-fluid" src={person1} alt="avatar" /></div>
                                            <div className="col-9">
                                                <p style={{ fontSize: "24px", margin: "0px" }}>Patience Toyosi</p>
                                                <p style={{ fontSize: "18px", color: "#81909D" }}>Facebook</p>
                                            </div>
                                        </div>

                                        <div className="card-body" style={{ color: "#3A434B" }}>
                                            <hr />
                                            <br />
                                            <p>
                                                “Completely beautiful website and amazing support! This is my second website from this author and I love both of the sites so much and she has helped me so well when I needed it!”</p>
                                        </div>
                                    </div>
                                </div>
                                <div className="col-md-6 col-sm-12 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                    <div className="card  shadow pt-3 pb-5 px-2" style={{ height: "auto" }}>
                                        <div className="row card-icon">
                                            <div className="col-3"><img className="avatar" src={person2} alt="avatar" /></div>
                                            <div className="col-9">
                                                <p style={{ fontSize: "24px", margin: "0px" }}>Patience Toyosi</p>
                                                <p style={{ fontSize: "18px", color: "#81909D" }}>Facebook</p>
                                            </div>

                                        </div>

                                        <div className="card-body" style={{ color: "#3A434B" }}>
                                            <hr />
                                            <br />
                                            <p>
                                                “Completely beautiful website and amazing support! This is my second website from this author and I love both of the sites so much and she has helped me so well when I needed it!”</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

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

                            <div className="col-sm-12">

                                <div className="js-form-message form-group">
                                    <label htmlFor={"lastName"} className="input-label">Portfolio Link</label>
                                    <input type="text" className="form-control" name="lastName" id="lastName" placeholder="https://drive.google.com/wind/bcdubcdhjcedcdc" aria-label="Gaga" required data-msg="Please enter last your name" />
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


        </ >
    );
}

export default AppHomePage;