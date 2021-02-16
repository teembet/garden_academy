import CourseCard from "./course-card";

export interface CourseCardGridViewProps {
  programs: any;
}

const CourseCardGridView: React.SFC<CourseCardGridViewProps> = ({
  programs,
}) => {
  return (
    <>
      {programs.map((course: any, index: number) => (
        <CourseCard
          key={index}
          images={course.image}
          title={course.titlt}
          text={course.text}
          rating={course.rating}
          price={course.price}
        ></CourseCard>
      ))}
    </>
  );
};

export default CourseCardGridView;
