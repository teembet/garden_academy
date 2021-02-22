import { Link } from "react-router-dom";

import Rating from "./rating";
import "../assets/css/programs.css";

export interface CourseCardProps {
  course: any;
  grid: number;
}

const CourseCard: React.SFC<CourseCardProps> = ({ course, grid }) => {
  return (
    <>
      <div
        className={
          "col-sm-6 col-lg-" + grid + " px-2 px-lg-3 mb-3 mb-lg-0 mt-3"
        }
      >
        <Link
          to={{
            pathname: "/details",
            state: {
              data: course,
            },
          }}
        >
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
              src={course.avatar}
              alt="product design"
              style={{
                width: "100%",
                maxHeight: "10rem",
                minHeight: "10rem",
                objectFit: "cover",
              }}
            />
            <div className="card-body">
              <p className="product-title" style={{ minHeight: "4.5rem" }}>
                {<b>{course.name}</b>}
              </p>
              <p
                className="products"
                dangerouslySetInnerHTML={{
                  __html:
                    course.description?.length > 45
                      ? course.description?.substring(0, 45) + "..."
                      : course.description,
                }}
                style={{ minHeight: "3rem" }}
              ></p>
              {/*   <p className="stars">
                {course.star_count
                  ? Math.round(parseInt(course.star_count))
                  : "1"} </p> */}
              <Rating rating={course.star_count}></Rating>

              <p className="amount">
                ₦ {Math.round(parseInt(course.price))}{" "}
                <span
                  style={{ float: "right", color: "#000", fontSize: ".8em" }}
                >
                  <i className="far fa-clock"></i>&nbsp;{course.duration}
                </span>
              </p>
            </div>
          </div>
        </Link>
      </div>
    </>
  );
};

export default CourseCard;
