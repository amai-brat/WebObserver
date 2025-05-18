import type { FC } from "react";
import { FaPlus } from 'react-icons/fa';

interface CornerButtonProps {
  onClick: () => void;
}

export const CornerButton: FC<CornerButtonProps> = ({ onClick }) => {
  return (
    <button
        onClick={onClick}
        className="fixed bottom-8 right-8 bg-primary-darker text-white p-4 rounded-full 
                 shadow-lg hover:bg-blue-700 transition-colors duration-200"
      >
        <FaPlus size={24} />
      </button>
  );
}