import { Link } from "react-router-dom";

import Rating from "./rating";
import "../assets/css/programs.css";

export interface CourseCardProps {
  images: string;
  title: string;
  text: string;
  rating: string;
  price: string;
  grid: number;
}

const CourseCard: React.SFC<CourseCardProps> = ({
  images,
  title,
  text,
  rating,
  price,
  grid,
}) => {
  return (
    <>
      <div
        className={
          "col-sm-6 col-lg-" + grid + " px-2 px-lg-3 mb-3 mb-lg-0 mt-3"
        }
      >
        <Link to="/details">
          <div
            className="card"
            style={{
              padding: "0",
              borderRadius: "4%",
              border: "1px solid #D7DCE0",
              boxSizing: "border-box",
              height: "inherit",
            }}
          >
            <img
              className="img-fluid card-img-top"
              src={images}
              alt="product design"
              style={{ width: "100%" }}
            />
            <div className="card-body">
              <p className="product-title">
                <b>{title}</b>
              </p>
              <p className="products">
                {text.length > 40 ? text.substring(0, 40) + "..." : text}
              </p>
              <p className="stars">
                {rating} <Rating rating={Math.round(parseInt(rating))}></Rating>
              </p>
              <p className="amount">{price}</p>
            </div>
          </div>
        </Link>
      </div>
    </>
  );
};

export default CourseCard;
