import { useParams } from "react-router-dom";

export const ObservingPage = () => {
  const { id } = useParams();

  return (<div>
    {id}
  </div>);
}