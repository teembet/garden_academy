import { useEffect, useState } from "react";

export interface SearchProps {
  search: string;
  button_text: string;
  onSearchSubmit: any;
  searchData: string;
}

const Search: React.SFC<SearchProps> = ({
  search,
  button_text,
  onSearchSubmit,
  searchData = "",
}) => {
  const [searchTerm, setSearchTerm] = useState("");

  useEffect(() => {
    if (searchData) {
      setSearchTerm(searchData);
    }
  }, []);

  return (
    <>
      <form className="input-group">
        <input
          type="search"
          className="search-placeholder form-control d-none d-lg-block d-md-block"
          placeholder={" 🔍 " + search}
          aria-label="Search Front"
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />

        <input
          type="search"
          className="search-placeholder form-control d-block d-lg-none d-md-none"
          placeholder={" 🔍 " + search}
          aria-label="Search Front"
          style={{ fontSize: "0.7em", height: "auto" }}
          value={searchTerm}
          onChange={(e) => setSearchTerm(e.target.value)}
        />

        <div className="input-group-append">
          <button
            style={{ width: "125px" }}
            type="button"
            onClick={() => {
              onSearchSubmit(searchTerm);
            }}
            className="btn btn-primary "
          >
            {button_text}
          </button>
        </div>
      </form>
    </>
  );
};

export default Search;
