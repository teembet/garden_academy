import '../assets/css/course-detailspage.css'
import { FaStar } from "react-icons/fa";
import bitmap from "../assets/img/bitmap.png";

import PaymentOptions from '../components/payment-options'






export interface AppCourseDetailsProps {

}

const AppCourseDetails: React.SFC<AppCourseDetailsProps> = () => {
    return (
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
                                        <button type="submit" className="btn btn-block btn-primary transition-3d-hover">Submit</button>
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
    );
}

export default AppCourseDetails;