import rect from "../assets/img/payment_icon.svg";
export interface PaymentOptionsProps {}

const PaymentOptions: React.SFC<PaymentOptionsProps> = ({ children }) => {
  return (
    <>
      <div
        className="position-relative pay"
        style={{ padding: "0 3%", margin: "40px 5%" }}
      >
        <div className="d-lg-flex align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
          {/* <!-- Card --> */}
          <section className=" ">
            <div style={{ marginTop: "40px" }}>
              <h1 className="font-weight-bold ">Payment Options</h1>
              <p style={{ marginTop: "10px", textAlign: "left" }}>
                We offer payment options to accomodate our users needs
                <br />
                Because we realise some of you no get money like that
              </p>

              <div className="row">
                <div className="col-md-3 mb-3">
                  <div className="card-icon">
                    <span>
                      <img src={rect} alt="" />
                    </span>
                  </div>
                  <h4 className="card-headers ">One off payment </h4>
                  <div className="card-body">
                    Get your course payments out of the way at once with our
                    one-off option. No fuss, no hassle.
                  </div>
                </div>
                <div className="col-md-3 mb-3">
                  <div className="card-icon">
                    <span>
                      <img src={rect} alt="" />
                    </span>
                  </div>
                  <h4 className="card-headers ">Installmental Payment</h4>
                  <div className="card-body ">
                    If the one-off option is inconvenient, this option is
                    available to enable you to spread your payments in specified
                    portions over a period.
                  </div>
                </div>
                <div className="col-md-3 mb-3">
                  <div className="card-icon">
                    <span>
                      <img src={rect} alt="" />
                    </span>
                  </div>
                  <h4 className="card-headers">Loan Offer</h4>
                  <div className="card-body ">
                    Take advantage of the EduCollect(insert hyperlink) platform
                    to gain access to collateral-free funding to allow you
                    proceed with your learning.
                  </div>
                </div>
                <div className="col-md-3 mb-3">
                  <div className="card-icon">
                    <span>
                      <img src={rect} alt="" />
                    </span>
                  </div>
                  <h4 className="card-headers">Deffered Income Share</h4>
                  <div className="card-body ">
                    And if you are unable to make use of any of the other
                    payment options due to financial difficulties, you can defer
                    your course payment till you secure employment. Terms &
                    Conditions apply
                  </div>
                </div>
              </div>
            </div>
          </section>
        </div>
        <br />

        {children}
      </div>
    </>
  );
};

export default PaymentOptions;
