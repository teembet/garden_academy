import { Link } from "react-router-dom";

export interface NotFoundPageProps {}

const NotFoundPage: React.SFC<NotFoundPageProps> = () => {
  return (
    <>
      <div className="d-lg-flex align-items-lg-center space-top-2 space-lg-0 min-vh-lg-100">
        <div className="row" style={{ width: "100%" }}>
          <div className="col-6 offset-3">
            <h1 style={{ textAlign: "center" }}>Page Not Found</h1>
            <Link to="/" className="btn btn-primary btn-block">
              Go to Home
            </Link>
          </div>
        </div>
      </div>
    </>
  );
};

export default NotFoundPage;
