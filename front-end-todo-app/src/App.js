import logo from './logo.svg';
import './App.css';
import { useState, useEffect } from 'react';
import { todoAPI } from './services/api';

function App() {
  const [todos, setTodos] = useState([]); // State to hold the list of todos
  const [newTodo, setNewTodo] = useState(''); // State to hold the new todo input
  const [loading, setLoading] = useState(true); // State to handle loading state

  useEffect(() => {
    loadTodos();
  }, []); // Load todos on component mount

  const loadTodos = async () => {
    try {
      const response = await todoAPI.getAll();
      setTodos(response.data);
      setLoading(false); // Stop loading after data is fetched
    } catch (error) {
      console.error('Error loading todos:', error);
      setLoading(false); // Stop loading even if there's an error
    }
  };

  const addTodo = async () => {
    if (!newTodo.trim()) return; // Prevent adding empty todos
    try {
      const response = await todoAPI.create({
        title: newTodo,
        isCompleted: false
      });
      setTodos([...todos, response.data]); // Update state with new todo
      setNewTodo(''); // Clear input field
    } catch (error) {
      console.error('Error adding todo:', error);
    }
  };

  const toggleTodo = async (todo) => {
    try {
      const updated = { ...todo, isCompleted: !todo.isCompleted };
      await todoAPI.update(todo.id, updated);
      setTodos(todos.map(t => t.id === todo.id ? updated : t)); // Update state
    } catch (error) {
      console.error('Error toggling todo:', error);
    }
  };

  const deleteTodo = async (id) => {
    try {
      await todoAPI.delete(id);
      setTodos(todos.filter(t => t.id !== id)); // Update state
    } catch (error) {
      console.error('Error deleting todo:', error);
    }
  };

  if (loading) {
    return <div>Loading...</div>; // Show loading state
  }

  return (
    <div className="App">
            <h1>TO DO FLOW</h1>
            
            <div>
                <input
                    type="text"
                    value={newTodo}
                    onChange={(e) => setNewTodo(e.target.value)}
                    onKeyDown={(e) => e.key === 'Enter' && addTodo()}
                    placeholder="Add a new todo..."
                />
                <button onClick={addTodo}>Add</button>
            </div>

            <ul>
                {todos.map(todo => (
                    <li key={todo.id}>
                        <input
                            type="checkbox"
                            checked={todo.isCompleted}
                            onChange={() => toggleTodo(todo)}
                        />
                        <span style={{ 
                            textDecoration: todo.isCompleted ? 'line-through' : 'none' 
                        }}>
                            {todo.title}
                        </span>
                        <button onClick={() => deleteTodo(todo.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
}

export default App;
  