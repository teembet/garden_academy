import '../assets/css/coursedetails.css'
import rect from "../assets/img/rect-bg.svg";
import { FaStar } from "react-icons/fa";
import { FaStarHalfAlt } from "react-icons/fa"
import details from "../assets/img/details.svg"
export interface AppCourseDetailsProps {

}

const AppCourseDetails: React.SFC<AppCourseDetailsProps> = () => {
    return (
        <main id="content" role="main">
          <div>
<div className="d-lg-flex position-relative heros" >
                    <div className="container d-lg-flex  align-items-lg-center space-top-2 space-lg-0 ">
                        {/* <!-- Content --> */}
                        <div className="w-md-100">
                            <div className="row">
                                <div className="col-lg-8">
                                    <div className="mb-5 mt-11">
                                        <h1 className="display-5 mb-3">
                                            User Experience Design Fundamentals
                                        </h1>
                                        <p className="">Design Web Sites and Mobile Apps that Your Users Love and Return to Again and Again with UX Expert Kingsley Omin.</p>
                                        
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                         <p>302,000 students</p>
                                    </div>

                                   
                                </div>
                                <div className="col-lg-4 mt-11">
<img className="img-fluid" src={details} alt=""/>
                                </div>

                              
                            </div>
                        </div>
                        {/* <!-- End Content -->

       
          {/* <!-- End SVG Shape --> */}
                    </div>
                </div>

                <div>
                    <div className="row">
                    <div className="col-lg-8">
                        <div className="container ">
                            <div className="card details">
                                <div className="head col-lg-6">
<h2 className="details-head">About the course you will learn</h2>
<p>Students should have basic computer skills and be comfortable navigating online.</p>
</div>
<div className="bullet col-lg-6">
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
                    <div className="col-lg-4">
                        <div className="container">
                            <div className="card"></div>
                        </div>
                    </div>
                    </div>
                </div>
          </div>
        </main>
    );
}

export default AppCourseDetails;