import type { FC, JSX } from "react";

interface CornerButtonProps {
  onClick: () => void;
  children: JSX.Element
}

export const CornerButton: FC<CornerButtonProps> = ({ onClick, children }) => {
  return (
    <button
        type="button"
        onClick={onClick}
        className="fixed bottom-8 right-8 bg-primary-darker text-white p-4 rounded-full 
                 shadow-lg hover:bg-blue-700 transition-colors duration-200"
      >
        {children}
      </button>
  );
}