
import * as React from 'react';
import AppHeader from '../components/header'
import AppFooter from '../components/footer'

import './homepage.css'
import rectangle from "../assets/img/rectangle.svg";
import homehero from "../assets/img/home-hero.svg";
import homehero2 from "../assets/img/home-hero2.svg";
import homecard1 from "../assets/img/homecard1.svg";
import homecard2 from "../assets/img/homecard2.svg";
import homecard3 from "../assets/img/homecard3.svg";
import homecard4 from "../assets/img/homecard4.svg";
import homecard5 from "../assets/img/homecard5.svg";
import homecard6 from "../assets/img/homecard6.svg";
import pd1 from "../assets/img/pd1.svg"
import pd2 from "../assets/img/pd2.svg"
import pd3 from "../assets/img/pd3.svg"
import facilitator from "../assets/img/facilitator.svg"
// import { HiOutlineLightBulb } from "react-icons/hi";

import { FaRegStar } from "react-icons/fa";
import { FaStarHalfAlt } from "react-icons/fa"

export interface HomePageProps {

}

const HomePage: React.SFC<HomePageProps> = () => {
    return (
        <React.Fragment>

            <AppHeader></AppHeader>

            {/* <!-- ========== MAIN CONTENT ========== --> */}
            <main id="content" role="main">
                {/* <!-- Hero Section --> */}
                <div className="d-lg-flex position-relative hero">
                    <div className="container d-lg-flex  align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
                        {/* <!-- Content --> */}
                        <div className="w-md-100">
                            <div className="row">
                                <div className="col-lg-5">
                                    <div className="mb-5 mt-11">
                                        <h1 className="display-4 mb-3">
                                           Learn the skills you need to succeed

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
                                        <p className="lead">We are committed to training the next generation of tech superstars and help organisations upscale
                        their workforce with the right talent</p>
                                    </div>

                                    <a className="btn btn-primary btn-wide transition-3d-hover" href="https://htmlstream.com/front/page-login-simple.html">Get Started</a>
                                    <a className="btn btn-link btn-wide" href="front.html#">Learn More <i className="fas fa-angle-right fa-sm ml-1"></i></a>
                                </div>
                                <div className="col-lg-7">
<img src={homehero} alt="" className="img-fluid"/> 
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
                                <span  className="span-icon"> <img src={homecard1} alt=""/> </span>
                            </div>
                            <h4 className="card-headers">Top Industry Facilitators</h4>
                            <div className="card-body">Lorem ipsum dolor sit amert plerusa consectetur vred adipiscing tortor donec deauteir accumsan nibh turpis treu massa,tortor benelit. </div>
                        </div>
                        
                            {/* <!-- End Card --> */}
                        </div>
                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3" >
                            {/* <!-- Card --> */}
                          <div className="card">
                            <div className="card-icon">
                                <span  className="span-icon"> <img src={homecard2} alt=""/> </span>
                            </div>
                            <h4 className="card-headers">Resume & Interview Prep</h4>
                            <div className="card-body">Lorem ipsum dolor sit amert plerusa consectetur vred adipiscing tortor donec deauteir accumsan nibh turpis treu massa,tortor benelit. </div>
                        </div>
                        
                            {/* <!-- End Card --> */}
                        </div>
                        
                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3" >
                            {/* <!-- Card --> */}
                          <div className="card">
                            <div className="card-icon">
                                <span  className="span-icon"> <img src={homecard3} alt=""/> </span>
                            </div>
                            <h4 className="card-headers">Flexible Learning</h4>
                            <div className="card-body">Lorem ipsum dolor sit amert plerusa consectetur vred adipiscing tortor donec deauteir accumsan nibh turpis treu massa,tortor benelit. </div>
                        </div>
                        
                            {/* <!-- End Card --> */}
                        </div>
                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3" >
                            {/* <!-- Card --> */}
                          <div className="card">
                            <div className="card-icon">
                                <span  className="span-icon"> <img src={homecard4} alt=""/> </span>
                            </div>
                            <h4 className="card-headers">Flexible Payment Options</h4>
                            <div className="card-body">Lorem ipsum dolor sit amert plerusa consectetur vred adipiscing tortor donec deauteir accumsan nibh turpis treu massa,tortor benelit. </div>
                        </div>
                        
                            {/* <!-- End Card --> */}
                        </div>
                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3" >
                            {/* <!-- Card --> */}
                          <div className="card">
                            <div className="card-icon">
                                <span  className="span-icon"> <img src={homecard5} alt=""/> </span>
                            </div>
                            <h4 className="card-headers">Globally Recognised Certificate</h4>
                            <div className="card-body">Lorem ipsum dolor sit amert plerusa consectetur vred adipiscing tortor donec deauteir accumsan nibh turpis treu massa,tortor benelit. </div>
                        </div>
                        
                            {/* <!-- End Card --> */}
                        </div>
                        <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3" >
                            {/* <!-- Card --> */}
                          <div className="card">
                            <div className="card-icon">
                                <span  className="span-icon"> <img src={homecard6} alt=""/> </span>
                            </div>
                            <h4 className="card-headers">Internship & Full-time opportunities</h4>
                            <div className="card-body">Lorem ipsum dolor sit amert plerusa consectetur vred adipiscing tortor donec deauteir accumsan nibh turpis treu massa,tortor benelit. </div>
                        </div>
                        
                            {/* <!-- End Card --> */}
                        </div>

                       

                    </div>
                </div>
                {/* <!-- End Articles Section --> */}
                <div className=" d-lg-flex position-relative session-three">
 <div className=" container d-lg-flex align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100" >
                            {/* <!-- Card --> */}
                           <section className=" ">
                <div className="img2-container mb-4">
                    <img className="img-fluid img2-style" src={homehero2} alt="" />
                </div>
                  <div>
                    <h1 className="font-weight-bold white-text">Looking To Hire Tech Talent Or Upskill Existing Employees For Your Organisation</h1>
                    <p style={{ marginTop: "20px", textAlign: "left" }}>Our custom agile process puts you in control; reduces risk, increases transparency between our team and yours, and allows you to hit even aggressive timelines.</p>

                    <div className="row" style={{ marginTop: "70px" }}>
                        <div className="col-md-4 mb-3" style={{ padding: "0px" }}>
                            <div className="card-icon">
                                <span>
                                    <img src={rectangle} alt="" />
                                </span>
                            </div>
                            <h4 className="card-headers white-text">Hire Talent</h4>
                            <div className="card-body s3-para">
                                Lorem ipsum dolor sit amet, bretsas consectetur adipiscing elit. Eget tortor, pellentesque donec ut Ornare accumsan nibh turpis eu volutpat. Ornare vel massa facilisis Ornare pellentesque donecrec bear rebto pellentesque amet.                    </div>
                        </div>
                        <div className="col-md-4 mb-3">
                            <div className="card-icon">
                                <span><img src={rectangle} alt="" /> </span>
                            </div>
                            <h4 className="card-headers white-text">Upskill Existing Staff</h4>
                            <div className="card-body s3-para">
                                Lorem ipsum dolor sit amet, bretsas consectetur adipiscing elit. Eget tortor, pellentesque donec ut Ornare accumsan nibh turpis eu volutpat. Ornare vel massa facilisis Ornare pellentesque donecrec bear rebto pellentesque amet.                    </div>
                        </div>
                        <div className="col-md-4 mb-3">
                            <div className="card-icon">
                                <span><img src={rectangle} alt="" /> </span>
                            </div>
                            <h4 className="card-headers white-text">Staff Onboarding</h4>
                            <div className="card-body s3-para">
                                Lorem ipsum dolor sit amet, bretsas consectetur adipiscing elit. Eget tortor, pellentesque donec ut Ornare accumsan nibh turpis eu volutpat. Ornare vel massa facilisis Ornare pellentesque donecrec bear rebto pellentesque amet.                    </div>
                        </div>
                    </div>

                    <div className="get-started">
                        <button className="btn get-started-btn">Get Started</button>
                    </div>

                </div>
                </section>
                            {/* <!-- End Card --> */}
                        </div>
</div>
   
               <div className="session-four mt-3">
                <div className="session-two-writeup">
                    <h3 style={{ fontSize: "36px", color: "#041644" }}>Available Programs</h3>
                    <p>Select any program from our library of carefully crafted programs guaranted to take you </p>
                </div>
                <div className="row mx-n2 mx-lg-n3">
                    <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                        <div className="card">
                            <img className="card-img-top svg" src={pd1} alt="product design" />
                            <div className="card-body">
                                <p className="product-title"><b>Product Design</b></p>
                                <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                <p className="stars">4.5 <FaRegStar className="star" /><FaRegStar className="star" /><FaRegStar className="star" /><FaRegStar className="star" /><FaStarHalfAlt className="star" /></p>
                                <p className="amount">NGN250,000</p>
                            </div>
                        </div>
                    </div>

                    <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                        <div className="card">
                            <img className="card-img-top svg" src={pd2} alt="product design" />
                            <div className="card-body">
                                <p className="product-title"><b>Product Design</b></p>
                                <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                <p className="stars">4.5 <FaRegStar className="star" /><FaRegStar className="star" /><FaRegStar className="star" /><FaRegStar className="star" /><FaStarHalfAlt className="star" /></p>
                                <p className="amount">NGN250,000</p>
                            </div>
                        </div>
                    </div>

                    <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                        <div className="card">
                            <img className="card-img-top svg" src={pd3} alt="product design" />
                            <div className="card-body">
                                <p className="product-title"><b>Product Design</b></p>
                                <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                <p className="stars">4.5 <FaRegStar className="star" /><FaRegStar className="star" /><FaRegStar className="star" /><FaRegStar className="star" /><FaStarHalfAlt className="star" /></p>
                                <p className="amount">NGN250,000</p>
                            </div>
                        </div>
                    </div>


                </div>
                <div className="get-started">
                    <button className="btn programs-btn"><b>View All Programs</b></button>
                </div>

            </div>
                
{/* CONTINUE FROM HERE ABEG*/ }
{/* CONTINUE FROM HERE ABEG*/ }
{/* CONTINUE FROM HERE ABEG*/ }
{/* CONTINUE FROM HERE ABEG*/ }
{/* CONTINUE FROM HERE ABEG*/ }
{/* CONTINUE FROM HERE ABEG*/ }
{/* CONTINUE FROM HERE ABEG*/ }
{/* CONTINUE FROM HERE ABEG*/ }
{/* CONTINUE FROM HERE ABEG*/ }

               {/* <div className="session-five d-lg-flex position-relative">
                <div className="row container d-lg-flex align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
                    <div className="col-md-6 mt-5">
                        <hr className="mt-5" style={{ width: "10%", border: "2px solid #0B2253", opacity: "0.5", margin: "0px" }} />
                        <h3 className="mt-3" style={{ fontSize: "36px", color: "#041644" }}>Become a Facilator</h3>
                        <p className="mt-3 facillator-p">Lorem ipsum dolor sit amet, consectetur vred adipiscing tortor, pellentesque donec deaut accumsan nibh turpis eu massa consectetur adipiscing tortor benelit.
                       </p>
                        <p className="facillator-p">
                            Lorem ipsum dolor sit amet, consectetur vred adipiscing adipiscing tortor, pellentesque donec deaut accumsan nibh turpis pellentesque donec deaut consectetur.
                   </p>

                        <button className="btn facillator-btn">Become a Facillator</button>


                    </div>

                    <div className="col-md-6 mt-5">
                        <img className="img-fluid" src={facilitator} alt="facillator" />
                    </div>
                </div>
            </div> */}

            {/* <section className="session-six">
                <div className="row">
                    <div className="col-md-4 mt-5">
                        <h3 className="mt-3 testimonials-heading" style={{textAlign:'center'  }}>Testimonials</h3>
                        <p className="mt-3" style={{ fontSize: "35px", textAlign:'center'  }}>Read what our users have to say...</p>

                    </div>

                    <div className="col-md-8 mt-5 testimonials-card">
                        <div className="card mr-4 shadow pt-3 pb-5 px-2">
                            <div className="row card-icon">
                                <div className="col-3"><img className="avatar" src="" alt="avatar" /></div>
                                <div className="col-9">
                                    <p >Patience Toyosi</p>
                                    <p>Facebook</p>
                                </div>
                            </div>
                            <hr />
                            <div className="card-body" style={{ fontSize: "18px", color: "#3A434B" }}>
                                “Completely beautiful website and amazing support! This is my second website from this author and I love both of the sites so much and she has helped me so well when I needed it!”
                       </div>
                        </div>
                        <div className="card mr-4 shadow pt-3 pb-5 px-2">
                            <div className="row card-icon">
                                <div className="col-3"><img className="avatar" src="" alt="avatar" /></div>
                                <div className="col-9">
                                    <p >Patience Toyosi</p>
                                    <p >Facebook</p>
                                </div>
                            </div>
                            <hr />
                            <div className="card-body" style={{ fontSize: "18px", color: "#3A434B" }}>
                                “Completely beautiful website and amazing support! This is my second website from this author and I love both of the sites so much and she has helped me so well when I needed it!”
                       </div>
                        </div>
                    </div>
                </div>
            </section> */}
            </main >
            {/*  < !-- ========== END MAIN CONTENT ========== --> */}

            <AppFooter></AppFooter>

        </React.Fragment>
    );
}

export default HomePage;