import '../assets/css/programs.css'
import rect from "../assets/img/rect-bg.svg";
import { FaStar } from "react-icons/fa";
import { FaStarHalfAlt } from "react-icons/fa"
import pd1 from "../assets/img/pd1.svg";
import pd2 from "../assets/img/pd2.svg";
import pd3 from "../assets/img/pd3.svg";
export interface AppProgramsPageProps {

}

const AppProgramsPage: React.SFC<AppProgramsPageProps> = () => {
    return (
        <main id="content" role="main">
            <div>
                <div className="d-lg-flex position-relative heros" >
                    <div className="container d-lg-flex  align-items-lg-center space-top-2 space-lg-0 ">
                        {/* <!-- Content --> */}
                        <div className="w-md-100">
                            <div className="row lead">
                                <div className="col-lg-6">
                                    <div className="mb-5 mt-11">
                                        <h1 className="display-4 mb-3">
                                            Featured Courses
                                        </h1>
                                        <p className="">Choose from any of our wide range of courses tailored to suit your needs.</p>
                                        <p className="">From software engineering to product management and more, We have you covered</p>
                                    </div>

                                    <form className="input-group">
                                        <input type="search" className="form-control" placeholder="&#128269;  What do you want to learn" aria-label="Search Front" />
                                        <div className="input-group-append">
                                            <button style={{ width: "125px" }} type="button" className="btn btn-primary">Search</button>
                                        </div>
                                    </form>
                                    <br /><br />
                                </div>


                            </div>
                        </div>
                        {/* <!-- End Content -->

       
          {/* <!-- End SVG Shape --> */}
                    </div>
                </div>

                <div className="session-four container space-2 space-top-xl-3 space-bottom-lg-3">

                    <section>
                        <div className="row mx-n2 mx-lg-n3">
                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img className="img-fluid card-img-top" src={pd1} alt="product design" style={{ width: "100%" }} />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img style={{ width: "100%" }} className="img-fluid card-img-top" src={pd2} alt="product design" />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img style={{ width: "100%" }} className="img-fluid card-img-top" src={pd3} alt="product design" />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img className="img-fluid card-img-top" src={pd1} alt="product design" style={{ width: "100%" }} />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img style={{ width: "100%" }} className="img-fluid card-img-top" src={pd2} alt="product design" />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img style={{ width: "100%" }} className="img-fluid card-img-top" src={pd3} alt="product design" />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img className="img-fluid card-img-top" src={pd1} alt="product design" style={{ width: "100%" }} />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img style={{ width: "100%" }} className="img-fluid card-img-top" src={pd2} alt="product design" />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img style={{ width: "100%" }} className="img-fluid card-img-top" src={pd3} alt="product design" />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img className="img-fluid card-img-top" src={pd1} alt="product design" style={{ width: "100%" }} />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img style={{ width: "100%" }} className="img-fluid card-img-top" src={pd2} alt="product design" />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img style={{ width: "100%" }} className="img-fluid card-img-top" src={pd3} alt="product design" />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img className="img-fluid card-img-top" src={pd1} alt="product design" style={{ width: "100%" }} />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img style={{ width: "100%" }} className="img-fluid card-img-top" src={pd2} alt="product design" />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                            <div className="col-sm-6 col-lg-4 px-2 px-lg-3 mb-3 mb-lg-0 mt-3">
                                <div className="card" style={{
                                    padding: "0", borderRadius: "", border: "1px solid #D7DCE0", boxSizing: "border-box"
                                }}>
                                    <img style={{ width: "100%" }} className="img-fluid card-img-top" src={pd3} alt="product design" />
                                    <div className="card-body">
                                        <p className="product-title"><b>Product Design</b></p>
                                        <p className="products">Learn how to design products that users will love. Product Design ble..... </p>
                                        <p className="stars">4.5 <FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStar className="star" /><FaStarHalfAlt className="star" /></p>
                                        <p className="amount">NGN250,000</p>
                                    </div>
                                </div>
                            </div>

                        </div>
                    </section>



                </div>
                <div className="d-lg-flex position-relative pay" style={{ padding: "0 3%", margin: "0 10%" }}>
                    <div className="  d-lg-flex align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100" >
                        {/* <!-- Card --> */}
                        <section className=" ">

                            <div style={{ marginTop: "40px" }}>
                                <h1 className="font-weight-bold ">Payment Options</h1>
                                <p style={{ marginTop: "20px", textAlign: "left" }}>We offer payment options to accomodate our users needs</p>
                                <p style={{ marginTop: "20px", textAlign: "left" }}>Because we realise some of you no get money like that</p>

                                <div className="row" style={{ marginTop: "70px" }}>
                                    <div className="col-md-3 mb-3" style={{ padding: "0px" }}>
                                        <div className="card-icon">
                                            <span>
                                                <img src={rect} alt="" />
                                            </span>
                                        </div>
                                        <h4 className="card-headers ">One off payment </h4>
                                        <div className="card-body">
                                            Feel free to use these in any private or public space. Please do not repackage and redistribute these as your own.</div>
                                    </div>
                                    <div className="col-md-3 mb-3">
                                        <div className="card-icon">
                                            <span><img src={rect} alt="" /> </span>
                                        </div>
                                        <h4 className="card-headers ">Installmental Payment</h4>
                                        <div className="card-body ">
                                            Feel free to use these in any private or public space. Please do not repackage and redistribute these as your own.</div>
                                    </div>
                                    <div className="col-md-3 mb-3">
                                        <div className="card-icon">
                                            <span><img src={rect} alt="" /> </span>
                                        </div>
                                        <h4 className="card-headers">Loan Offer</h4>
                                        <div className="card-body ">
                                            Feel free to use these in any private or public space. Please do not repackage and redistribute these as your own.</div>
                                    </div>
                                    <div className="col-md-3 mb-3">
                                        <div className="card-icon">
                                            <span><img src={rect} alt="" /> </span>
                                        </div>
                                        <h4 className="card-headers">Deffered Income Share</h4>
                                        <div className="card-body ">
                                            Feel free to use these in any private or public space. Please do not repackage and redistribute these as your own.</div>
                                    </div>
                                </div>



                            </div>
                        </section>
                        {/* <!-- End Card --> */}
                    </div>
                </div >
            </div>
        </main>
    );
}

export default AppProgramsPage;