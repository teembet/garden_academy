import { FaStar } from "react-icons/fa";

export interface RatingProps {
  rating: string;
}

const Rating: React.SFC<RatingProps> = ({ rating = "1.00" }) => {
  const ratingArray = Array.from(
    Array(Math.round(parseInt(rating ? rating : "1.00"))).keys()
  );

  return (
    <>
      {ratingArray.map((rate, index) => {
        return <FaStar key={index} className="star" />;
      })}
    </>
  );
};

export default Rating;
