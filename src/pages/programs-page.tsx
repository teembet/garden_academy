import { Link } from 'react-router-dom';

import '../assets/css/programs.css'
import { FaStar } from "react-icons/fa";
import { FaStarHalfAlt } from "react-icons/fa"
import pd1 from "../assets/img/pd1.svg";
import pd2 from "../assets/img/pd2.svg";
import pd3 from "../assets/img/pd3.svg";
import PaymentOptions from '../components/payment-options'
import CourseCard from '../components/course-card'
import Search from '../components/search'
export interface AppProgramsPageProps {

}

const AppProgramsPage: React.SFC<AppProgramsPageProps> = () => {
    const searchCourse = () => {
        console.log("wind")
    }

    return (
        <main id="content" role="main">
            <div>

                <div className="hero-page-about">
                    <h1>Featured Courses</h1>

                    <p>Choose from any of our wide range of courses tailored to suit your needs.
                        <br /> From software engineering to product management and more, We have got you covered</p>

                    <div className="row" style={{ width: "100%" }}>
                        <div className="col-md-6 offset-md-3 ">
                            <Search search={"What do you want to learn"} button_text={"Search"} onSearchSubmit={searchCourse} ></Search>
                        </div>
                    </div>

                </div>
                <br />

                <div className="session-four container space-2 space-top-xl-3 space-bottom-lg-3">

                    <section>
                        <div className="row mx-n2 mx-lg-n3">

                            <CourseCard images={pd1} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd2} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd3} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd1} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd2} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd3} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd1} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd2} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd3} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd1} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd2} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd3} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd1} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd2} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd3} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd1} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd2} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                            <CourseCard images={pd3} title={"Product Design"} text={"Learn how to design products that users will love. Product Design ble..... "} rating={"4.5"} price={"NGN250,000"} ></CourseCard>

                        </div>

                    </section>
                </div>
            </div>
            <PaymentOptions>

            </PaymentOptions>

        </main>
    );
}

export default AppProgramsPage;