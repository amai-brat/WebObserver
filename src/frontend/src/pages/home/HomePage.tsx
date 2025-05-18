import { CornerButton } from "./ui/CornerButton";
import { ObservingsTable } from "./ui/ObservingsTable";

export const HomePage = () => {
  return <div className="m-2">
    <ObservingsTable />
    <CornerButton onClick={() => console.log('aa')}/>
  </div>;
}