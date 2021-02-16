import { useEffect, useState } from "react";
import { FaStar } from "react-icons/fa";

export interface RatingProps {
  rating: number;
}

const Rating: React.SFC<RatingProps> = ({ rating = 1 }) => {
  const [ratingArray, setRatingAray] = useState([0]);

  useEffect(() => {
    let array = [];
    for (let i = 0; i < rating; i++) {
      array.push(i);
    }
    setRatingAray(array);
  }, []);

  return (
    <>
      {ratingArray.map((rate, index) => {
        return <FaStar key={index} className="star" />;
      })}
    </>
  );
};

export default Rating;
