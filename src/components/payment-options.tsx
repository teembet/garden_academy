
import rect from "../assets/img/rect-bg.svg";
export interface PaymentOptionsProps {

}

const PaymentOptions: React.SFC<PaymentOptionsProps> = () => {
    return (
        <>
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
        </>
    );
}

export default PaymentOptions;