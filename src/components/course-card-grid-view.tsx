import CourseCard from "./course-card";

export interface CourseCardGridViewProps {
  programs: any;
  grid: number;
}

const CourseCardGridView: React.SFC<CourseCardGridViewProps> = ({
  programs,
  grid,
}) => {
  return (
    <>
      {programs.map((course: any, index: number) => (
        <CourseCard grid={grid} key={index} course={course}></CourseCard>
      ))}
    </>
  );
};

export default CourseCardGridView;
