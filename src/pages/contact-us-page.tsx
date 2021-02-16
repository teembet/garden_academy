import React from 'react';
import '../assets/css/contactpage.css'
import Socials from '../components/socials';
export interface AppContactUsPageProps {

}

const AppContactUsPage: React.SFC<AppContactUsPageProps> = () => {
    return (
        <main id="content" role="main">

            <div className="hero-page">
                <h1>Contact Us</h1>
                <p>&nbsp;</p>
                <p>&nbsp;</p>
                <p>&nbsp;</p>

            </div>
            <br />
            <div className="container space-top-6 space-top-lg-4 space-bottom-2">
                <div className="card" style={{ height: "inherit", padding: "0 5%" }}>
                    <div className="row ">
                        <div className="space-right-3 space-top-3 space-bottom-2 col-md-6">

                            <div className="mb-4">
                                <h2 className="text-primary">You can reach us via any of these mediums.</h2>
                            </div>

                            <ul className="nav nav-sm nav-x-0 nav-white flex-column">
                                <h5 >Telephone</h5>
                                <li className="nav-item">
                                    <a className="nav-link media" href="tel:+2348036670001">
                                        <span className="media">
                                            <span className="fas fa-phone-alt mt-1 mr-2"></span>
                                            <span className="media-bodys">
                                                +234 [0] 803 667 0001 &nbsp;&nbsp;  +234 [0] 803 667 0001
                      </span>
                                        </span>
                                    </a>
                                </li>
                                <br />

                                <h5 >Email Address</h5>
                                <li className="nav-item">
                                    <a className="nav-link media" href="mailto:contactus@gardenacademy.com">
                                        <span className="media">
                                            <span className="fas fa-phone-alt mt-1 mr-2"></span>
                                            <span className="media-bodys">
                                                contactus@gardenacademy.com
                      </span>
                                        </span>
                                    </a>
                                </li>
                                <br />
                                <h5 >Office Address</h5>
                                <li className="nav-item">
                                    <a className="nav-link media" href="#">
                                        <span className="media">
                                            <span className="fas fa-location-arrow mt-1 mr-2"></span>
                                            <span className="media-bodys">
                                                Vibranium Vally
                                                42, Local Airport Road, Ikeja
                                                Lagos
                                         </span>
                                        </span>
                                    </a>
                                </li>


                            </ul>
                            <br />
                            <Socials></Socials>
                        </div >

                        <div className="space-top-2 space-bottom-2 col-md-6">


                            <form className="js-validate shadow-lg mb-4">


                                <div className="card-body p-4 p-md-6">
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

                                        <div className="col-sm-12">

                                            <div className="js-form-message form-group">
                                                <label htmlFor={"emailAddress"} className="input-label">Email address</label>
                                                <input type="email" className="form-control" name="emailAddress" id="emailAddress" placeholder="eg. Kingsleyomin@gmail.com" aria-label="alex@pixeel.com" required data-msg="Please enter a valid email address" />
                                            </div>

                                        </div>

                                        <div className="col-sm-12">

                                            <div className="js-form-message form-group">
                                                <label htmlFor={"emailAddress"} className="input-label">Subject</label>
                                                <input type="text" className="form-control" name="emailAddress" id="emailAddress" placeholder="eg. Facilitator enquiry" aria-label="alex@pixeel.com" required data-msg="Please enter a valid email address" />
                                            </div>

                                        </div>

                                        <div className="col-sm-12">

                                            <div className="js-form-message form-group">
                                                <label htmlFor={"message"} className="input-label">How can we help you</label>
                                                <div className="input-group">
                                                    <textarea className="form-control" rows={4} name="message" id="message" placeholder="Enter message here" aria-label="Hi there, I would like to ..." required data-msg="Please enter a reason."></textarea>
                                                </div>
                                            </div>

                                        </div>
                                    </div>

                                    <button type="submit" className="btn btn-block btn-primary transition-3d-hover">Submit</button>
                                </div>
                            </form>

                        </div>
                    </div>


                </div>

            </div>
        </main>
    );
}

export default AppContactUsPage;